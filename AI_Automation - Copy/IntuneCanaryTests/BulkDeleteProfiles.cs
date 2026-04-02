using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class BulkDeleteProfiles : PageTest
    {
        private ExtentTest? _test;
        private string? _bearerToken;

        /// <summary>
        /// Maps friendly baseline keys to their Intune portal display names.
        /// Used to resolve Graph API template IDs for baseline-specific deletion.
        /// </summary>
        private static readonly Dictionary<string, string> BaselineDisplayNames = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Win365"]            = "Windows 365 Security Baseline",
            ["Win10"]             = "Security Baseline for Windows 10 and later",
            ["Edge"]              = "Security Baseline for Microsoft Edge",
            ["Defender"]          = "Microsoft Defender for Endpoint Security Baseline",
            ["M365Apps"]          = "Microsoft 365 Apps for Enterprise Security Baseline",
            ["HoloLens2"]         = "Standard Security Baseline for HoloLens 2",
            ["HoloLens2Advanced"] = "Advanced Security Baseline for HoloLens 2",
        };

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoSH_09.pfx"));
            Console.WriteLine($"Certificate path: {certPath}");
            return new BrowserNewContextOptions
            {
                ClientCertificates = new[] {
                    new ClientCertificate {
                        Origin = "https://certauth.login.microsoftonline.com",
                        PfxPath = certPath,
                        Passphrase = "Admin@123"
                    }
                }
            };
        }

        [SetUp]
        public void TestSetup()
        {
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Bulk Delete Automation Profiles via Graph API");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        /// <summary>
        /// Intercepts a Graph API request from the Intune portal to capture the bearer token.
        /// </summary>
        /// <summary>
        /// Decodes a JWT and checks whether its audience includes graph.microsoft.com.
        /// </summary>
        private static bool IsGraphToken(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3) return false;
                var payload = parts[1].Replace('-', '+').Replace('_', '/');
                var mod = payload.Length % 4;
                if (mod == 2) payload += "==";
                else if (mod == 3) payload += "=";
                var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                return json.Contains("graph.microsoft.com");
            }
            catch { return false; }
        }

        /// <summary>Checks whether the JWT scp/roles claim includes DeviceManagement permissions.</summary>
        private static bool IsDeviceManagementToken(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3) return false;
                var payload = parts[1].Replace('-', '+').Replace('_', '/');
                var mod = payload.Length % 4;
                if (mod == 2) payload += "==";
                else if (mod == 3) payload += "=";
                var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(payload));
                return json.Contains("DeviceManagement") || json.Contains("deviceManagement");
            }
            catch { return false; }
        }

        private async Task<string> CaptureGraphToken(IPage page)
        {
            string? token = null;

            // Register route handler to intercept Graph API calls — validate audience
            await page.RouteAsync("**/graph.microsoft.com/**", async route =>
            {
                var headers = route.Request.Headers;
                if (headers.TryGetValue("authorization", out var auth) && auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var candidate = auth.Substring("Bearer ".Length);
                    if (IsGraphToken(candidate) && IsDeviceManagementToken(candidate))
                    {
                        token = candidate;
                        Console.WriteLine("  ✓ Graph token captured via route intercept (DeviceManagement scope validated)");
                    }
                    else if (IsGraphToken(candidate) && string.IsNullOrEmpty(token))
                    {
                        token = candidate; // Accept any graph token as fallback
                        Console.WriteLine("  ✓ Graph token captured via route intercept (audience validated)");
                    }
                }
                await route.ContinueAsync();
            });

            // Navigate to pages that trigger DeviceManagement Graph API calls
            try
            {
                // First navigate to Devices — this triggers DeviceManagement.Read.All scoped tokens
                await page.GotoAsync("https://intune.microsoft.com/#view/Microsoft_Intune_DeviceSettings/DevicesMenu/~/overview");
                await page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 20000 });
                await page.WaitForTimeoutAsync(3000);

                // Also navigate to Security Baselines to cover intents endpoint tokens
                var epLink = page.Locator("a:has-text('Endpoint security'), button:has-text('Endpoint security')").First;
                await epLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await epLink.ClickAsync();
                await page.WaitForTimeoutAsync(3000);

                var sbLink = page.Locator("a:has-text('Security baselines'), button:has-text('Security baselines')").First;
                await sbLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await sbLink.ClickAsync();
                await page.WaitForTimeoutAsync(5000);
            }
            catch { }

            // Unregister route
            await page.UnrouteAsync("**/graph.microsoft.com/**");

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("  Route intercept did not capture a valid Graph token — scanning storage for graph.microsoft.com-scoped token...");

                // Fallback: scan session + local storage for a token whose JWT audience is graph.microsoft.com
                foreach (var frame in page.Frames)
                {
                    try
                    {
                        token = await frame.EvaluateAsync<string>(@"() => {
                            function decodeJwtPayload(t) {
                                try {
                                    const p = t.split('.')[1].replace(/-/g,'+').replace(/_/g,'/');
                                    return JSON.parse(atob(p));
                                } catch { return null; }
                            }
                            function isGraphToken(t) {
                                if (!t || !t.startsWith('eyJ')) return false;
                                const pl = decodeJwtPayload(t);
                                if (!pl || !pl.aud) return false;
                                const aud = Array.isArray(pl.aud) ? pl.aud : [pl.aud];
                                return aud.some(a => a && a.includes('graph.microsoft.com'));
                            }
                            function scanStorage(storage) {
                                for (let i = 0; i < storage.length; i++) {
                                    try {
                                        const val = storage.getItem(storage.key(i));
                                        if (!val) continue;
                                        const parsed = JSON.parse(val);
                                        // MSAL token cache entries
                                        const candidates = [parsed.secret, parsed.access_token, parsed.token].filter(Boolean);
                                        for (const c of candidates) {
                                            if (isGraphToken(c)) return c;
                                        }
                                    } catch {}
                                }
                                return null;
                            }
                            return scanStorage(sessionStorage) || scanStorage(localStorage);
                        }");
                        if (!string.IsNullOrEmpty(token))
                        {
                            Console.WriteLine($"  ✓ Graph token found in browser storage (frame: {frame.Name ?? "main"})");
                            break;
                        }
                    }
                    catch { }
                }
            }

            return token ?? throw new Exception("Could not capture a graph.microsoft.com-scoped bearer token. Ensure the portal has made at least one Graph API call.");
        }

        // =====================================================================
        //  TEST METHODS — one per baseline type + one for all
        // =====================================================================

        /// <summary>Deletes ALL Automation_ profiles across every endpoint (original behavior).</summary>
        [Test]
        public async Task BulkDelete_AllAutomationProfiles()
        {
            await DeleteAutomationProfiles(baselineFilter: null);
        }

        /// <summary>Deletes Automation_ profiles only under the Windows 365 Security Baseline.</summary>
        [Test]
        public async Task BulkDelete_Win365Profiles()
        {
            await DeleteAutomationProfiles(baselineFilter: new[] { "Win365" });
        }

        /// <summary>Deletes Automation_ profiles only under the Security Baseline for Windows 10 and later.</summary>
        [Test]
        public async Task BulkDelete_Win10Profiles()
        {
            await DeleteAutomationProfiles(baselineFilter: new[] { "Win10" });
        }

        /// <summary>Deletes Automation_ profiles under both Windows 365 AND Windows 10 and later baselines.</summary>
        [Test]
        public async Task BulkDelete_Win365_And_Win10_Profiles()
        {
            await DeleteAutomationProfiles(baselineFilter: new[] { "Win365", "Win10" });
        }

        /// <summary>Deletes Automation_ profiles only under the Microsoft Edge Security Baseline.</summary>
        [Test]
        public async Task BulkDelete_EdgeProfiles()
        {
            await DeleteAutomationProfiles(baselineFilter: new[] { "Edge" });
        }

        /// <summary>Deletes Automation_ profiles only under the Microsoft Defender for Endpoint Security Baseline.</summary>
        [Test]
        public async Task BulkDelete_DefenderProfiles()
        {
            await DeleteAutomationProfiles(baselineFilter: new[] { "Defender" });
        }

        /// <summary>Deletes Automation_ profiles only under the Microsoft 365 Apps for Enterprise Security Baseline.</summary>
        [Test]
        public async Task BulkDelete_M365AppsProfiles()
        {
            await DeleteAutomationProfiles(baselineFilter: new[] { "M365Apps" });
        }

        /// <summary>Deletes Automation_ profiles only under the Standard Security Baseline for HoloLens 2.</summary>
        [Test]
        public async Task BulkDelete_HoloLens2Profiles()
        {
            await DeleteAutomationProfiles(baselineFilter: new[] { "HoloLens2" });
        }

        /// <summary>Deletes Automation_ profiles only under the Advanced Security Baseline for HoloLens 2.</summary>
        [Test]
        public async Task BulkDelete_HoloLens2AdvancedProfiles()
        {
            await DeleteAutomationProfiles(baselineFilter: new[] { "HoloLens2Advanced" });
        }

        // =====================================================================
        //  HELPER: Resolve baseline display names → Graph template IDs
        // =====================================================================

        /// <summary>
        /// Queries GET /beta/deviceManagement/templates and returns the set of template IDs
        /// whose displayName matches one of the requested baseline keys.
        /// </summary>
        private async Task<HashSet<string>> ResolveTemplateIds(HttpClient httpClient, string[] baselineKeys)
        {
            var targetDisplayNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var key in baselineKeys)
            {
                if (BaselineDisplayNames.TryGetValue(key, out var displayName))
                    targetDisplayNames.Add(displayName);
                else
                    Console.WriteLine($"  ⚠ Unknown baseline key '{key}', skipping. Valid keys: {string.Join(", ", BaselineDisplayNames.Keys)}");
            }

            if (targetDisplayNames.Count == 0)
                throw new Exception($"No valid baseline keys provided. Valid keys: {string.Join(", ", BaselineDisplayNames.Keys)}");

            Console.WriteLine($"  Resolving template IDs for: {string.Join(", ", targetDisplayNames)}");

            var templateIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string? nextUri = "https://graph.microsoft.com/beta/deviceManagement/templates?$top=100";

            while (!string.IsNullOrEmpty(nextUri))
            {
                try
                {
                    var response = await httpClient.GetAsync(nextUri);
                    var content = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"    ✗ Failed to query templates: {response.StatusCode}");
                        break;
                    }

                    var json = JsonDocument.Parse(content);
                    if (json.RootElement.TryGetProperty("value", out var values))
                    {
                        foreach (var item in values.EnumerateArray())
                        {
                            string? name = null;
                            if (item.TryGetProperty("displayName", out var dn) && dn.ValueKind == JsonValueKind.String)
                                name = dn.GetString();

                            if (name != null && targetDisplayNames.Any(t => name.Contains(t, StringComparison.OrdinalIgnoreCase)))
                            {
                                var id = item.GetProperty("id").GetString()!;
                                templateIds.Add(id);
                                Console.WriteLine($"    ✓ Template matched: '{name}' → {id}");
                            }
                        }
                    }

                    nextUri = json.RootElement.TryGetProperty("@odata.nextLink", out var next) ? next.GetString() : null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"    ✗ Error querying templates: {ex.Message}");
                    break;
                }
            }

            if (templateIds.Count == 0)
                Console.WriteLine("  ⚠ No matching templates found — no intents will be filtered by template");

            return templateIds;
        }

        // =====================================================================
        //  CORE: Login → Token → Query → Filter → Delete
        // =====================================================================

        /// <summary>
        /// Core deletion method.
        /// - baselineFilter = null  → deletes ALL Automation_ profiles across all 6 endpoints (original behavior).
        /// - baselineFilter = ["Win365"] → deletes only intents whose templateId belongs to Windows 365.
        /// - baselineFilter = ["Win365","Win10"] → deletes intents matching either baseline.
        /// </summary>
        private async Task DeleteAutomationProfiles(string[]? baselineFilter)
        {
            string profilePrefix = "Automation_";
            string filterDesc = baselineFilter != null
                ? string.Join(" + ", baselineFilter.Select(k => BaselineDisplayNames.TryGetValue(k, out var n) ? n : k))
                : "ALL endpoints";

            try
            {
                Console.WriteLine("=== BULK DELETE AUTOMATION PROFILES VIA GRAPH API ===");
                Console.WriteLine($"Prefix: {profilePrefix}");
                Console.WriteLine($"Filter: {filterDesc}");

                // ============ STEP 1: LOGIN ============
                Console.WriteLine("\n--- Step 1: Login ---");
                _test?.Info("Step 1: Login");
                var securityBaseline = new SecurityBaseline();

                for (int loginAttempt = 1; loginAttempt <= 3; loginAttempt++)
                {
                    Console.WriteLine($"Login attempt {loginAttempt}/3...");
                    try
                    {
                        await securityBaseline.Login(Page);
                    }
                    catch (Exception loginEx)
                    {
                        Console.WriteLine($"  Login error: {loginEx.Message}");
                        if (loginAttempt < 3)
                        {
                            await Task.Delay(20000);
                            try { await Page.GotoAsync("https://intune.microsoft.com", new PageGotoOptions { Timeout = 60000 }); } catch { await Task.Delay(10000); }
                            continue;
                        }
                        throw new Exception($"Login failed after 3 attempts: {loginEx.Message}", loginEx);
                    }

                    try { await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 }); } catch { }
                    await Page.WaitForTimeoutAsync(5000);

                    if (Page.Url.Contains("/Error/") || Page.Url.Contains("SigninFailed"))
                    {
                        Console.WriteLine($"  Sign-in failed, URL: {Page.Url}");
                        if (loginAttempt < 3) { await Task.Delay(15000); await Page.GotoAsync("https://intune.microsoft.com"); continue; }
                        throw new Exception($"Login failed - error page: {Page.Url}");
                    }

                    Console.WriteLine($"  Login OK! URL: {Page.Url}");
                    _test?.Pass("Logged in");
                    break;
                }

                // ============ STEP 2: CAPTURE BEARER TOKEN ============
                Console.WriteLine("\n--- Step 2: Capture Graph API bearer token ---");
                _bearerToken = await CaptureGraphToken(Page);
                Console.WriteLine($"  ✓ Token captured ({_bearerToken.Length} chars, starts with {_bearerToken.Substring(0, 20)}...)");
                _test?.Info("Bearer token captured from browser session");

                // ============ STEP 3: QUERY PROFILES VIA GRAPH API ============
                Console.WriteLine($"\n--- Step 3: Query profiles via Graph API (Filter: {filterDesc}) ---");

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");

                // Resolve template IDs when filtering by specific baseline(s)
                HashSet<string>? allowedTemplateIds = null;
                HashSet<string>? allowedDisplayNames = null;
                if (baselineFilter != null)
                {
                    allowedTemplateIds = await ResolveTemplateIds(httpClient, baselineFilter);
                    Console.WriteLine($"  Resolved {allowedTemplateIds.Count} template ID(s) for filtering");

                    // Also collect display names for configurationPolicies matching (different template ID scheme)
                    allowedDisplayNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var key in baselineFilter)
                    {
                        if (BaselineDisplayNames.TryGetValue(key, out var dn))
                            allowedDisplayNames.Add(dn);
                    }
                }

                // When filtering by baseline, query both intents (legacy) AND configurationPolicies (Settings Catalog / newer baselines).
                // Newer security baselines (e.g., Windows 365) are stored as configurationPolicies, NOT intents.
                // When no filter, query all endpoint types.
                var endpoints = new Dictionary<string, string>();

                if (baselineFilter != null)
                {
                    endpoints["intents"] = "https://graph.microsoft.com/beta/deviceManagement/intents?$top=100";
                    endpoints["configurationPolicies"] = "https://graph.microsoft.com/beta/deviceManagement/configurationPolicies?$top=100";
                }
                else
                {
                    endpoints["intents"] = "https://graph.microsoft.com/beta/deviceManagement/intents?$top=100";
                    endpoints["configurationPolicies"] = "https://graph.microsoft.com/beta/deviceManagement/configurationPolicies?$top=100";
                    endpoints["deviceConfigurations"] = "https://graph.microsoft.com/beta/deviceManagement/deviceConfigurations?$top=100";
                    endpoints["groupPolicyConfigurations"] = "https://graph.microsoft.com/beta/deviceManagement/groupPolicyConfigurations?$top=100";
                    endpoints["compliancePolicies"] = "https://graph.microsoft.com/beta/deviceManagement/compliancePolicies?$top=100";
                    endpoints["deviceCompliancePolicies"] = "https://graph.microsoft.com/beta/deviceManagement/deviceCompliancePolicies?$top=100";
                }

                // Collect matching profiles
                var allProfiles = new List<(string endpoint, string name, string id)>();

                foreach (var ep in endpoints)
                {
                    Console.WriteLine($"\n  Querying {ep.Key}...");
                    string? nextUri = ep.Value;
                    int count = 0;
                    int matchCount = 0;

                    while (!string.IsNullOrEmpty(nextUri))
                    {
                        try
                        {
                            var response = await httpClient.GetAsync(nextUri);
                            var content = await response.Content.ReadAsStringAsync();

                            if (!response.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"    ✗ {response.StatusCode}: {content.Substring(0, Math.Min(content.Length, 150))}");
                                break;
                            }

                            var json = JsonDocument.Parse(content);
                            if (json.RootElement.TryGetProperty("value", out var values))
                            {
                                foreach (var item in values.EnumerateArray())
                                {
                                    count++;
                                    // Try common name properties
                                    string? name = null;
                                    foreach (var prop in new[] { "displayName", "name" })
                                    {
                                        if (item.TryGetProperty(prop, out var val) && val.ValueKind == JsonValueKind.String)
                                        {
                                            name = val.GetString();
                                            break;
                                        }
                                    }

                                    // Diagnostic: log all profile names found (first 10 per endpoint)
                                    if (name != null && count <= 10)
                                        Console.WriteLine($"    [#{count}] Found: '{name}'");

                                    if (name == null) continue;

                                    if (name.StartsWith(profilePrefix, StringComparison.OrdinalIgnoreCase))
                                    {
                                        // When filtering by baseline, check template ID
                                        if (allowedTemplateIds != null)
                                        {
                                            string? templateId = null;
                                            string? templateDisplayName = null;

                                            if (ep.Key == "intents")
                                            {
                                                // Legacy intents use top-level templateId
                                                if (item.TryGetProperty("templateId", out var tid) && tid.ValueKind == JsonValueKind.String)
                                                    templateId = tid.GetString();
                                            }
                                            else if (ep.Key == "configurationPolicies")
                                            {
                                                // Settings Catalog policies use templateReference with different ID scheme
                                                if (item.TryGetProperty("templateReference", out var tref) && tref.ValueKind == JsonValueKind.Object)
                                                {
                                                    if (tref.TryGetProperty("templateId", out var tid2) && tid2.ValueKind == JsonValueKind.String)
                                                        templateId = tid2.GetString();
                                                    if (tref.TryGetProperty("templateDisplayName", out var tdn) && tdn.ValueKind == JsonValueKind.String)
                                                        templateDisplayName = tdn.GetString();
                                                    // Also check templateFamily
                                                    if (tref.TryGetProperty("templateFamily", out var tf) && tf.ValueKind == JsonValueKind.String)
                                                        Console.WriteLine($"    → templateFamily={tf.GetString()}");
                                                }
                                            }

                                            Console.WriteLine($"    → '{name}' templateId={templateId ?? "(none)"}, templateDisplayName={templateDisplayName ?? "(none)"}");

                                            bool matched = false;

                                            // Check 1: Exact template ID match (for intents)
                                            if (templateId != null && allowedTemplateIds.Contains(templateId))
                                                matched = true;

                                            // Check 2: Versioned template ID match (e.g., "cef15778-..._1")
                                            if (!matched && templateId != null && allowedTemplateIds.Any(aid => templateId.StartsWith(aid, StringComparison.OrdinalIgnoreCase)))
                                            {
                                                Console.WriteLine($"    → Versioned templateId match: {templateId}");
                                                matched = true;
                                            }

                                            // Check 3: templateDisplayName contains the baseline display name (for configurationPolicies)
                                            if (!matched && templateDisplayName != null && allowedDisplayNames != null)
                                            {
                                                if (allowedDisplayNames.Any(dn => templateDisplayName.Contains(dn, StringComparison.OrdinalIgnoreCase)
                                                    || dn.Contains(templateDisplayName, StringComparison.OrdinalIgnoreCase)))
                                                {
                                                    Console.WriteLine($"    → Matched by templateDisplayName: '{templateDisplayName}'");
                                                    matched = true;
                                                }
                                            }

                                            // Check 4: For configurationPolicies, skip template filtering if no intents template matched
                                            //          and just match by prefix (Automation_) since the user specifically asked for this baseline
                                            if (!matched && ep.Key == "configurationPolicies" && allowedTemplateIds.Count == 0)
                                            {
                                                Console.WriteLine($"    → No template IDs resolved, accepting by prefix match only");
                                                matched = true;
                                            }

                                            if (!matched)
                                            {
                                                Console.WriteLine($"    → Skipped (templateId mismatch)");
                                                continue;
                                            }
                                        }

                                        var id = item.GetProperty("id").GetString()!;
                                        allProfiles.Add((ep.Key, name, id));
                                        matchCount++;
                                        if (matchCount <= 10) Console.WriteLine($"    ✓ MATCH: {name} ({id})");
                                    }
                                }
                            }

                            nextUri = json.RootElement.TryGetProperty("@odata.nextLink", out var next) ? next.GetString() : null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"    ✗ Error: {ex.Message}");
                            break;
                        }
                    }

                    Console.WriteLine($"    Total: {count}, Matching '{profilePrefix}*': {matchCount}");
                }

                Console.WriteLine($"\n  === TOTAL MATCHING PROFILES: {allProfiles.Count} ({filterDesc}) ===");

                if (allProfiles.Count == 0)
                {
                    Console.WriteLine("  Nothing to delete!");
                    _test?.Pass($"No Automation_ profiles found for [{filterDesc}] — already clean");
                    return;
                }

                // Show all matches grouped by endpoint
                foreach (var grp in allProfiles.GroupBy(p => p.endpoint))
                {
                    Console.WriteLine($"\n  [{grp.Key}] — {grp.Count()} profiles:");
                    foreach (var p in grp.Take(10)) Console.WriteLine($"    {p.name} ({p.id})");
                    if (grp.Count() > 10) Console.WriteLine($"    ... and {grp.Count() - 10} more");
                }

                // ============ STEP 4: DELETE MATCHING PROFILES ============
                Console.WriteLine($"\n--- Step 4: Deleting {allProfiles.Count} profiles via Graph API ---");
                _test?.Info($"Deleting {allProfiles.Count} profiles [{filterDesc}]");

                // Map endpoint name to Graph API delete path
                var deleteBasePaths = new Dictionary<string, string>
                {
                    ["intents"] = "https://graph.microsoft.com/beta/deviceManagement/intents",
                    ["configurationPolicies"] = "https://graph.microsoft.com/beta/deviceManagement/configurationPolicies",
                    ["deviceConfigurations"] = "https://graph.microsoft.com/beta/deviceManagement/deviceConfigurations",
                    ["groupPolicyConfigurations"] = "https://graph.microsoft.com/beta/deviceManagement/groupPolicyConfigurations",
                    ["compliancePolicies"] = "https://graph.microsoft.com/beta/deviceManagement/compliancePolicies",
                    ["deviceCompliancePolicies"] = "https://graph.microsoft.com/beta/deviceManagement/deviceCompliancePolicies",
                };

                int deleted = 0;
                int failed = 0;
                var sw = System.Diagnostics.Stopwatch.StartNew();

                foreach (var profile in allProfiles)
                {
                    var deleteUri = $"{deleteBasePaths[profile.endpoint]}/{profile.id}";

                    try
                    {
                        var delResponse = await httpClient.DeleteAsync(deleteUri);

                        if (delResponse.IsSuccessStatusCode || delResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            deleted++;
                            Console.WriteLine($"  ✓ [{deleted}] Deleted: {profile.name} ({profile.endpoint})");
                        }
                        else
                        {
                            var errBody = await delResponse.Content.ReadAsStringAsync();
                            failed++;
                            Console.WriteLine($"  ✗ Failed: {profile.name} — {delResponse.StatusCode}: {errBody.Substring(0, Math.Min(errBody.Length, 200))}");
                        }
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        Console.WriteLine($"  ✗ Error: {profile.name} — {ex.Message}");
                    }
                }

                sw.Stop();

                // ============ SUMMARY ============
                Console.WriteLine($"\n========================================");
                Console.WriteLine($"  FILTER: {filterDesc}");
                Console.WriteLine($"  DELETED: {deleted}  |  FAILED: {failed}  |  TIME: {sw.Elapsed.TotalSeconds:F1}s");
                Console.WriteLine($"========================================\n");

                _test?.Info($"Summary: {deleted} deleted, {failed} failed in {sw.Elapsed.TotalSeconds:F1}s [{filterDesc}]");

                if (deleted > 0) _test?.Pass($"Deleted {deleted} profiles via Graph API [{filterDesc}]");
                else _test?.Warning("No profiles were deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FATAL: {ex.Message}\n{ex.StackTrace}");
                _test?.Fail($"Error: {ex.Message}");
                try { var ss = await ExtentReportHelper.CaptureScreenshot(Page, "BulkDelete_Error"); if (!string.IsNullOrEmpty(ss)) _test?.Fail("Error", MediaEntityBuilder.CreateScreenCaptureFromPath(ss).Build()); } catch { }
                throw;
            }
        }
    }
}
