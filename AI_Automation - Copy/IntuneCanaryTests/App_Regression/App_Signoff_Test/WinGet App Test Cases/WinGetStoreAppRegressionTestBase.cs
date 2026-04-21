using LogService;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using global::PlaywrightTests.Common.Model;
using global::PlaywrightTests.Common.Utils.BaseUtils.Apps.ByPlatform;
using global::PlaywrightTests.Common.Utils.BaseUtils.UtilInterface;
using global::PlaywrightTests.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace IntuneCanaryTests
{
    public abstract class WinGetStoreAppRegressionTestBase : PageTest
    {
        private ExtentTest? _test;
        private SmartStepExecutor? _smartStep;

        protected abstract string RegressionTestCaseId { get; }

        protected abstract string TestDisplayName { get; }

        private string NumericTestId => RegressionTestCaseId.Replace("TC_", string.Empty, StringComparison.OrdinalIgnoreCase);

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
            Console.WriteLine($"Certificate path: {certPath}");
            return new BrowserNewContextOptions
            {
                ClientCertificates = new[] {
                    new ClientCertificate {
                        Origin = "https://certauth.login.microsoftonline.com/c0219094-a70e-402c-8dd2-fd89f7d64010/certauth",
                        PfxPath = certPath,
                        Passphrase = "Admin@123"
                    }
                }
            };
        }

        [SetUp]
        public void TestSetup()
        {
            // Load .env file so GOOGLE_AI_API_KEY is available for AI-powered healing
            var envPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ".env"));
            if (File.Exists(envPath))
            {
                DotNetEnv.Env.Load(envPath);
                Console.WriteLine($"[ENV] Loaded .env from: {envPath}");
                Console.WriteLine($"[ENV] GOOGLE_AI_API_KEY set: {!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GOOGLE_AI_API_KEY"))}");
            }

            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, TestDisplayName);
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info($"Test ID: {NumericTestId}");
            _test.Info("Category: WinGet App Test Cases");
        }

        [TearDown]
        public void TestTearDown()
        {
            _test?.Info($"Test completed at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            // Dump self-healing report so agent and console can see what was healed
            SelfHealingLocator.DumpHealingReport();
        }

        protected async Task RunTestAsync()
        {
            string createdAppName = string.Empty;

            try
            {
                Console.WriteLine($"{GetType().Name} started...");
                _test?.Info("Test execution started");

                var testData = LoadTestData();
                var assignmentMode = GetAssignmentMode(testData.Parameters.Assignments);
                ValidateTestData(testData, assignmentMode);

                _test?.Info($"Loaded regression data for {testData.Parameters.DisplayName} and group {testData.Parameters.Assignments.SelectGroups}");

                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.Login(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Login_Complete_{NumericTestId}");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                var environment = ResolveEnvironment(Page.Url);
                var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                var allAppsUtils = new AllAppsUtils(Page, environment);
                _smartStep = new SmartStepExecutor(allAppsUtils, testData.Parameters.AppType);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Open All Apps",
                    new ControlInfo { ControlType = "GoToMainPageAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Start app creation",
                    new ControlInfo { ControlType = "ClickAddButtonAsync" });

                await SelectAppTypeDirectlyAsync(testData.Parameters.AppType);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Select Store app {testData.Parameters.SearchApp}",
                    new ControlInfo
                    {
                        ControlType = "SelectTheMicrosoftStoreAppNewAsync",
                        OperationValue = testData.Parameters.SearchApp
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app display name",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationNameAsync",
                        OperationValue = testData.Parameters.DisplayName
                    });

                createdAppName = parameters.TryGetValue("AppAutomationAppName", out var uniqueName)
                    ? uniqueName
                    : testData.Parameters.DisplayName;

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app description",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationDescriptionAsync",
                        OperationValue = testData.Parameters.Description
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Set app publisher",
                    new ControlInfo
                    {
                        ControlType = "SetAppInformationPublisherAsync",
                        OperationValue = testData.Parameters.Publisher
                    });

                // Install behavior is disabled in current portal UI - skip setting it
                _test?.Info($"Skipping Install behavior ({testData.Parameters.Assignments.InstallContext}) - field is disabled in current portal version.");


                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Continue to assignments",
                    new ControlInfo { ControlType = "ClickNextButtonAsync" });

                parameters = await ConfigureAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Mark assignment stage complete",
                    new ControlInfo { ControlType = "MarkAssignmentsCompleteAsync" });

                    parameters = await ExecuteStepAsync(
                        allAppsUtils,
                        parameters,
                        "Continue to review + create",
                        new ControlInfo { ControlType = "ClickNextButtonAsync" });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Create the Store app",
                    new ControlInfo { ControlType = "ClickCreateButtonWithoutWaitForUploadAsync" });

                await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
                await Page.WaitForTimeoutAsync(5000);

                var createScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Create_Complete_{NumericTestId}");
                if (!string.IsNullOrEmpty(createScreenshot))
                {
                    _test?.Info("App created", MediaEntityBuilder.CreateScreenCaptureFromPath(createScreenshot).Build());
                }

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Verify created app name",
                    new ControlInfo
                    {
                        ControlType = "VerifyPropertyAsync",
                        Value = new List<string> { "Name", createdAppName }
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Verify app description",
                    new ControlInfo
                    {
                        ControlType = "VerifyPropertyAsync",
                        Value = new List<string> { "Description", testData.Parameters.Description }
                    });

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Verify app publisher",
                    new ControlInfo
                    {
                        ControlType = "VerifyPropertyAsync",
                        Value = new List<string> { "Publisher", testData.Parameters.Publisher }
                    });

                parameters = await VerifyAssignmentsAsync(allAppsUtils, parameters, testData, assignmentMode);

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    "Mark verification complete",
                    new ControlInfo { ControlType = "SuccessAppAutomationVerifyResult" });

                _test?.Info(testData.Parameters.DeviceValidation.AppInstallationValidation);
                _test?.Pass("Test completed successfully!");
                Console.WriteLine("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, $"Error_{NumericTestId}");
                if (!string.IsNullOrEmpty(errorScreenshot))
                {
                    _test?.Fail($"Test failed with exception: {ex.Message}", MediaEntityBuilder.CreateScreenCaptureFromPath(errorScreenshot).Build());
                }
                else
                {
                    _test?.Fail($"Test failed with exception: {ex.Message}");
                }

                throw;
            }
            finally
            {
                // if (!string.IsNullOrWhiteSpace(createdAppName))  // TEMP: Skip cleanup to keep app
                // {
                //     await TryCleanupCreatedAppAsync(createdAppName);
                // }
            }
        }

        private async Task<Dictionary<string, string>> ConfigureAssignmentsAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            WinGetRegressionTestCase testData,
            AssignmentMode assignmentMode)
        {
            switch (assignmentMode)
            {
                case AssignmentMode.Required:
                    parameters = await ExecuteStepAsync(
                        utils,
                        parameters,
                        $"Assign required group {testData.Parameters.Assignments.SelectGroups}",
                        new ControlInfo
                        {
                            ControlType = "ClickRequiredAddGroupAsync",
                            OperationValue = testData.Parameters.Assignments.SelectGroups
                        });
                    break;

                case AssignmentMode.Available:
                    parameters = await ExecuteStepAsync(
                        utils,
                        parameters,
                        $"Assign available group {testData.Parameters.Assignments.SelectGroups}",
                        new ControlInfo
                        {
                            ControlType = "ClickAvailableForEnrolledDevicesAddGroupAsync",
                            OperationValue = testData.Parameters.Assignments.SelectGroups
                        });
                    break;

                case AssignmentMode.Uninstall:
                    parameters = await ExecuteStepAsync(
                        utils,
                        parameters,
                        $"Assign uninstall group {testData.Parameters.Assignments.SelectGroups}",
                        new ControlInfo
                        {
                            ControlType = "ClickUninstallAddGroupAsync",
                            OperationValue = testData.Parameters.Assignments.SelectGroups
                        });
                    break;
            }

            return parameters;
        }

        private async Task<Dictionary<string, string>> VerifyAssignmentsAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            WinGetRegressionTestCase testData,
            AssignmentMode assignmentMode)
        {
            var assignmentBehavior = GetAssignmentBehaviorName(assignmentMode);

            parameters = await ExecuteStepAsync(
                utils,
                parameters,
                $"Verify {assignmentBehavior.ToLowerInvariant()} assignment group",
                new ControlInfo
                {
                    ControlType = "VerifyPropertyAssignmentsAsync",
                    Value = new List<string> { assignmentBehavior, testData.Parameters.Assignments.SelectGroups }
                });

            // Skip Install behavior verification - field is disabled in current portal version.
            // Since SetOptionPickerAsync skips disabled fields, verification must also be skipped.
            if (!string.IsNullOrWhiteSpace(testData.Parameters.Assignments.InstallContext))
            {
                _test?.Info("Skipping 'Install behavior' verification - field is disabled in current portal version.");
            }
            return parameters;
        }

        private async Task<Dictionary<string, string>> ExecuteStepAsync(
            InterfaceUtils utils,
            Dictionary<string, string> parameters,
            string stepDescription,
            ControlInfo controlInfo)
        {
            _test?.Info(stepDescription);
            controlInfo.Parameter = parameters;
            try
            {
                // Use SmartStepExecutor when available (tab pre-check & retry on wrong-page)
                if (_smartStep != null && utils is AllAppsUtils)
                {
                    var smartResult = await _smartStep.ExecuteWithGuardsAsync(controlInfo);
                    return smartResult.Parameter;
                }

                var result = await utils.RunStepAsync(controlInfo);
                return result.Parameter;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HEAL_REQUEST] step={stepDescription} controlType={controlInfo.ControlType} error={ex.Message}");

                // Capture DOM for diagnostics (AI healing now runs inside SelfHealingLocator as Strategy 2-3)
                await CaptureDomAndAnalyzeAsync(stepDescription, controlInfo.ControlType, ex.Message);
                _test?.Fail($"Step failed: {stepDescription}  {ex.Message}");
                throw;
            }
        }

        private async Task SelectAppTypeDirectlyAsync(string appType)
        {
            _test?.Info($"Select app type {appType}");

            try
            {
                var appTypeComboBox = Page.GetByRole(AriaRole.Combobox, new() { Name = "App type", Exact = true });
                await appTypeComboBox.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 15000 });
                await appTypeComboBox.ClickAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HEAL_SIGNAL] SelectAppTypeDirectlyAsync: App type combobox not found: {ex.Message}");
                // Capture live DOM and ask Gemini for correct locator
                var domHtml = await CaptureDomOnFailureAsync("SelectAppType_ComboBox");
                var hints = new HealingHints
                {
                    Identifier = "AppTypeCombobox",
                    Text = "App type",
                    Role = AriaRole.Combobox,
                    AriaLabel = "App type"
                };
                var healed = await SelfHealingLocator.ResolveAsync(Page, Page.GetByRole(AriaRole.Combobox, new() { Name = "App type" }), hints, iframeName: null, timeoutMs: 15000);
                await healed.ClickAsync();
            }

            var option = Page.GetByText(new Regex($"^{Regex.Escape(appType)}$", RegexOptions.IgnoreCase)).First;
            await option.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await option.ClickAsync();

            var selectButton = Page.GetByRole(AriaRole.Button, new() { Name = "Select", Exact = true });
            await selectButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 30000 });
            await selectButton.ClickAsync();
        }

        /// <summary>
        /// Captures live DOM page source at moment of failure and saves to file.
        /// Like driver.getPageSource() in the Java Self_Healing project.
        /// </summary>
        private async Task<string> CaptureDomOnFailureAsync(string contextName)
        {
            try
            {
                string pageHtml = await Page.ContentAsync();
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var domDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "ExtentReports", "DomCaptures");
                Directory.CreateDirectory(domDir);
                var filePath = Path.Combine(domDir, $"DOM_{NumericTestId}_{contextName}_{timestamp}.html");
                await File.WriteAllTextAsync(filePath, pageHtml);
                Console.WriteLine($"[DOM_CAPTURE] Saved live DOM to: {filePath} ({pageHtml.Length} chars)");
                _test?.Info($"DOM captured at failure point: {contextName} ({pageHtml.Length} chars)");
                return pageHtml;
            }
            catch (Exception domEx)
            {
                Console.WriteLine($"[DOM_CAPTURE] Failed to capture DOM: {domEx.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Captures DOM and sends to Gemini AI to analyze the page.
        /// Phase 1: Single-element analysis (existing) via AILocatorHelper.FindLocatorAsync
        /// Phase 2: Java-style full page locator extraction via AIPageLocatorHelper.GetAllLocatorsAsync
        /// Outputs ALL page locators as JSON for diagnostic and self-healing use.
        /// </summary>
        private async Task CaptureDomAndAnalyzeAsync(string stepDescription, string controlType, string errorMessage)
        {
            try
            {
                string pageHtml = await CaptureDomOnFailureAsync(controlType);
                if (string.IsNullOrEmpty(pageHtml)) return;

                if (!AILocatorHelper.IsAvailable() && !AIPageLocatorHelper.IsAvailable())
                {
                    Console.WriteLine("[AI_DOM_ANALYSIS] GOOGLE_AI_API_KEY not set  skipping AI analysis");
                    return;
                }

                var hints = HealingHintsRegistry.Get(controlType) ?? new HealingHints
                {
                    Identifier = controlType,
                    Text = stepDescription
                };

                // Phase 1: Single-element AI analysis (existing Gemini single-locator pattern)
                if (AILocatorHelper.IsAvailable())
                {
                    Console.WriteLine($"[AI_DOM_ANALYSIS] Phase 1: Sending DOM ({pageHtml.Length} chars) to Gemini for step '{stepDescription}'...");
                    var aiResult = await AILocatorHelper.FindLocatorAsync(pageHtml, hints);
                    if (aiResult.HasValue)
                    {
                        var (response, elapsedMs) = aiResult.Value;
                        Console.WriteLine($"[AI_DOM_ANALYSIS] Phase 1 Result: {response.LocatorType}='{response.Locator}' in {elapsedMs}ms for step '{stepDescription}'");
                        _test?.Info($"AI DOM Analysis: Gemini suggests {response.LocatorType}='{response.Locator}' for '{controlType}'");
                    }
                    else
                    {
                        Console.WriteLine($"[AI_DOM_ANALYSIS] Phase 1: Gemini returned no result for step '{stepDescription}'");
                    }
                }

                // Phase 2: Java-style full page locator extraction (like OpenAIHelper.GetLocatorsForPageAsJson)
                if (AIPageLocatorHelper.IsAvailable())
                {
                    Console.WriteLine($"[AI_PAGE_SCAN] Phase 2: Extracting ALL page locators as JSON for step '{stepDescription}'...");
                    var pageResult = await AIPageLocatorHelper.GetAllLocatorsAsync(pageHtml, $"{controlType}_{stepDescription}");
                    if (pageResult.HasValue)
                    {
                        var (locators, rawJson, elapsedMs) = pageResult.Value;
                        Console.WriteLine($"[AI_PAGE_SCAN] Phase 2 Result: {locators.Count} locators extracted in {elapsedMs}ms");

                        // Output all locators as JSON (like Java testLoginForUserFromAI)
                        Console.WriteLine($"[AI_PAGE_LOCATORS_JSON] {{");
                        Console.WriteLine($"  \"pageContext\": \"{controlType}\",");
                        Console.WriteLine($"  \"failedStep\": \"{stepDescription}\",");
                        Console.WriteLine($"  \"totalLocators\": {locators.Count},");
                        Console.WriteLine($"  \"extractionTimeMs\": {elapsedMs},");
                        Console.WriteLine($"  \"locators\": [");
                        for (int i = 0; i < locators.Count; i++)
                        {
                            var loc = locators[i];
                            var comma = i < locators.Count - 1 ? "," : "";
                            Console.WriteLine($"    {{\"locatorName\":\"{EscapeJson(loc.LocatorName)}\",\"locatorType\":\"{loc.LocatorType}\",\"locator\":\"{EscapeJson(loc.Locator)}\"}}{comma}");
                        }
                        Console.WriteLine($"  ]");
                        Console.WriteLine($"}}");

                        // Save locators JSON to file alongside DOM capture
                        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        var domDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "ExtentReports", "DomCaptures");
                        var jsonPath = Path.Combine(domDir, $"LOCATORS_{NumericTestId}_{controlType}_{timestamp}.json");
                        await File.WriteAllTextAsync(jsonPath, rawJson);
                        Console.WriteLine($"[AI_PAGE_SCAN] Locators JSON saved to: {jsonPath}");
                        _test?.Info($"AI Page Scan: {locators.Count} locators extracted and saved for '{controlType}'");

                        // Build PageLocatorReader for immediate use in self-healing
                        var reader = new PageLocatorReader(Page);
                        reader.LoadLocators(locators);

                        // Try to find the failing element by fuzzy name match
                        var match = reader.FindPageLocatorByPartialName(hints.Text ?? hints.Identifier ?? controlType);
                        if (match != null)
                        {
                            Console.WriteLine($"[AI_PAGE_SCAN] Fuzzy match found for '{controlType}': {match.LocatorName} ({match.LocatorType}): {match.Locator}");
                            _test?.Info($"AI Page Scan: Suggested locator for '{controlType}': {match.LocatorType}='{match.Locator}'");
                        }
                        else
                        {
                            Console.WriteLine($"[AI_PAGE_SCAN] No fuzzy match found for '{controlType}' among {locators.Count} locators");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[AI_PAGE_SCAN] Phase 2: Failed to extract page locators for step '{stepDescription}'");
                    }
                }
            }
            catch (Exception analysisEx)
            {
                Console.WriteLine($"[AI_DOM_ANALYSIS] Analysis failed: {analysisEx.Message}");
            }
        }

        private static string EscapeJson(string s) => s?.Replace("\\", "\\\\").Replace("\"", "\\\"") ?? "";

        /// <summary>
        /// Maps controlType strings from RunStepAsync to HealingHintsRegistry keys.
        /// </summary>
        private static readonly Dictionary<string, string> ControlTypeToRegistryKey = new(StringComparer.OrdinalIgnoreCase)
        {
            ["ClickUninstallAddGroupAsync"] = "AddGroupLink_Uninstall",
            ["ClickRequiredAddGroupAsync"] = "AddGroupLink_Required",
            ["ClickAvailableForEnrolledDevicesAddGroupAsync"] = "AddGroupLink_Available",
            ["ClickAddButtonAsync"] = "CreateButton_CommandBar",
            ["ClickCreateButtonWithoutWaitForUploadAsync"] = "CreateButton_CommandBar",
        };

        /// <summary>
        /// Extracts a focused DOM section around a keyword context.
        /// Full DOM can be 900K+; Gemini only sees 50-80K truncated from the START.
        /// The failing element (e.g. Uninstall section) is often deep in the page.
        /// This extracts ~40K chars centered around the relevant context.
        /// </summary>
        private static string ExtractFocusedDom(string fullHtml, string contextKeyword)
        {
            if (string.IsNullOrEmpty(fullHtml) || string.IsNullOrEmpty(contextKeyword))
                return fullHtml;

            int idx = fullHtml.IndexOf(contextKeyword, StringComparison.OrdinalIgnoreCase);
            if (idx < 0) return fullHtml;

            // Extract ~40K chars centered on the keyword
            int halfWindow = 20000;
            int start = Math.Max(0, idx - halfWindow);
            int end = Math.Min(fullHtml.Length, idx + halfWindow);
            var focused = fullHtml[start..end];
            Console.WriteLine($"[AI_HEAL] Extracted focused DOM: {focused.Length} chars around '{contextKeyword}' (pos {idx} in {fullHtml.Length} total)");
            return focused;
        }

        /// <summary>
        /// Live AI self-healing: Captures DOM at failure, sends FOCUSED DOM to Gemini AI,
        /// gets the correct locator, and uses it to click the failed element.
        /// Like Java Self_Healing: OpenAIHelper.GetLocatorsForPageAsJson + LocatorReader + retry.
        /// </summary>
        private async Task<bool> TryAIHealAndRetryAsync(string stepDescription, string controlType, string errorMessage)
        {
            try
            {
                if (!AILocatorHelper.IsAvailable() && !AIPageLocatorHelper.IsAvailable())
                {
                    Console.WriteLine("[AI_HEAL] GOOGLE_AI_API_KEY not set  cannot attempt AI healing");
                    return false;
                }

                // Step 1: Capture live DOM (like Java driver.getPageSource())
                Console.WriteLine($"[AI_HEAL] Step 1: Capturing live DOM for healing step '{controlType}'...");
                string fullPageHtml = await CaptureDomOnFailureAsync(controlType);
                if (string.IsNullOrEmpty(fullPageHtml))
                {
                    Console.WriteLine("[AI_HEAL] DOM capture failed  cannot heal");
                    return false;
                }

                // Map controlType to registry key for proper hints
                string registryKey = ControlTypeToRegistryKey.TryGetValue(controlType, out var mapped) ? mapped : controlType;
                var hints = HealingHintsRegistry.Get(registryKey) ?? new HealingHints
                {
                    Identifier = controlType,
                    Text = stepDescription
                };
                Console.WriteLine($"[AI_HEAL] Using hints: Identifier='{hints.Identifier}', Text='{hints.Text}', ClassName='{hints.ClassName}', registryKey='{registryKey}'");

                // Extract focused DOM around the relevant section
                // For "Uninstall" assignment, focus on the Uninstall part of the DOM
                string focusKeyword = hints.Text ?? controlType;
                if (controlType.Contains("Uninstall", StringComparison.OrdinalIgnoreCase)) focusKeyword = "Uninstall";
                else if (controlType.Contains("Required", StringComparison.OrdinalIgnoreCase)) focusKeyword = "Required";
                else if (controlType.Contains("Available", StringComparison.OrdinalIgnoreCase)) focusKeyword = "Available for enrolled devices";

                string focusedHtml = ExtractFocusedDom(fullPageHtml, focusKeyword);

                // Step 2: Phase 1  Single element Gemini lookup with focused DOM
                if (AILocatorHelper.IsAvailable())
                {
                    Console.WriteLine($"[AI_HEAL] Step 2a: Sending focused DOM ({focusedHtml.Length} chars) to Gemini for '{hints.Identifier}'...");
                    var aiResult = await AILocatorHelper.FindLocatorAsync(focusedHtml, hints);
                    if (aiResult.HasValue)
                    {
                        var (response, elapsedMs) = aiResult.Value;
                        Console.WriteLine($"[AI_HEAL] Gemini suggests: {response.LocatorType}='{response.Locator}' in {elapsedMs}ms");
                        _test?.Info($"AI Healing: Gemini found {response.LocatorType}='{response.Locator}' for '{controlType}'");

                        // Try to use the AI-suggested locator
                        var aiLocator = Page.Locator(response.Locator);
                        try
                        {
                            await aiLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
                            await aiLocator.ScrollIntoViewIfNeededAsync();
                            await aiLocator.ClickAsync(new LocatorClickOptions { Timeout = 10000 });
                            Console.WriteLine($"[AI_HEAL] SUCCESS  Phase 1 Gemini locator clicked: {response.Locator}");
                            _test?.Pass($"[AI_HEALED] Gemini Phase 1 clicked element: {response.LocatorType}='{response.Locator}'");
                            return true;
                        }
                        catch (Exception clickEx)
                        {
                            Console.WriteLine($"[AI_HEAL] Phase 1 locator click failed: {clickEx.Message}. Trying Phase 2...");
                        }
                    }
                }

                // Step 3: Phase 2  Full page locator extraction with focused DOM
                if (AIPageLocatorHelper.IsAvailable())
                {
                    Console.WriteLine($"[AI_HEAL] Step 2b: Extracting ALL locators from focused DOM ({focusedHtml.Length} chars)...");
                    var pageResult = await AIPageLocatorHelper.GetAllLocatorsAsync(focusedHtml, $"{controlType}_{stepDescription}");
                    if (pageResult.HasValue)
                    {
                        var (locators, rawJson, elapsedMs) = pageResult.Value;
                        Console.WriteLine($"[AI_HEAL] Gemini extracted {locators.Count} locators in {elapsedMs}ms");

                        foreach (var loc in locators)
                            Console.WriteLine($"  [AI_LOCATOR] {loc.LocatorName} ({loc.LocatorType}): {loc.Locator}");

                        // Save locators
                        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        var domDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "ExtentReports", "DomCaptures");
                        Directory.CreateDirectory(domDir);
                        var jsonPath = Path.Combine(domDir, $"LOCATORS_{NumericTestId}_{controlType}_{timestamp}.json");
                        await File.WriteAllTextAsync(jsonPath, rawJson);

                        // Build reader and fuzzy match (like Java LocatorReader.findLocatorByPartialName)
                        var reader = new PageLocatorReader(Page);
                        reader.LoadLocators(locators);

                        string[] searchTerms = new[] { hints.Text, hints.AriaLabel, hints.Label, "Add group" }
                            .Where(s => !string.IsNullOrEmpty(s)).Distinct().ToArray();

                        foreach (var term in searchTerms)
                        {
                            var aiLocator = reader.FindByPartialName(term);
                            if (aiLocator != null)
                            {
                                try
                                {
                                    await aiLocator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
                                    await aiLocator.ScrollIntoViewIfNeededAsync();
                                    await aiLocator.ClickAsync(new LocatorClickOptions { Timeout = 10000 });
                                    var match = reader.FindPageLocatorByPartialName(term);
                                    Console.WriteLine($"[AI_HEAL] SUCCESS  Phase 2 matched '{term}' -> {match?.LocatorType}: {match?.Locator}");
                                    _test?.Pass($"[AI_HEALED] Gemini Phase 2 healed '{controlType}' via '{match?.LocatorType}={match?.Locator}'");
                                    return true;
                                }
                                catch (Exception matchEx)
                                {
                                    Console.WriteLine($"[AI_HEAL] Fuzzy match '{term}' click failed: {matchEx.Message}");
                                }
                            }
                        }

                        Console.WriteLine($"[AI_HEAL] Phase 2: No matching locator clicked among {locators.Count} locators");
                    }
                }

                Console.WriteLine($"[AI_HEAL] All AI healing attempts failed for '{controlType}'");
                return false;
            }
            catch (Exception healEx)
            {
                Console.WriteLine($"[AI_HEAL] Healing exception: {healEx.Message}");
                return false;
            }
        }


        /// <summary>
        private async Task TryCleanupCreatedAppAsync(string createdAppName)
        {
            try
            {
                var environment = ResolveEnvironment(Page.Url);
                var allAppsUtils = new AllAppsUtils(Page, environment);
                var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    ["AppAutomationAppName"] = createdAppName
                };

                parameters = await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Cleanup created app {createdAppName}",
                    new ControlInfo { ControlType = "GoToMainPageAsync" });

                await ExecuteStepAsync(
                    allAppsUtils,
                    parameters,
                    $"Delete app {createdAppName}",
                    new ControlInfo { ControlType = "DeleteAppByNameAsync" });
            }
            catch (Exception cleanupEx)
            {
                _test?.Warning($"Cleanup failed for app '{createdAppName}': {cleanupEx.Message}");
            }
        }

        private static string ResolveEnvironment(string currentUrl)
        {
            if (currentUrl.Contains("intuneSH", StringComparison.OrdinalIgnoreCase) ||
                currentUrl.Contains("/sh/", StringComparison.OrdinalIgnoreCase))
            {
                return "SH";
            }

            if (currentUrl.Contains("intuneCanary", StringComparison.OrdinalIgnoreCase) ||
                currentUrl.Contains("canary", StringComparison.OrdinalIgnoreCase))
            {
                return "CTIP";
            }

            return "PE";
        }

        private static WinGetRegressionTestCase LoadTestData(string? regressionTestCaseId = null)
        {
            var testDataPath = Path.GetFullPath(Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..",
                "..",
                "..",
                "..",
                "TestData_AppReggersion",
                "WinGet_StoreApp_Regression.json"));

            using var stream = File.OpenRead(testDataPath);
            var root = JsonSerializer.Deserialize<WinGetRegressionRoot>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var targetTestCaseId = regressionTestCaseId ?? throw new InvalidOperationException("Regression test case id is required.");

            return root?.TestCases?.FirstOrDefault(testCase =>
                       string.Equals(testCase.TestCaseId, targetTestCaseId, StringComparison.OrdinalIgnoreCase))
                   ?? throw new InvalidOperationException($"Test case {targetTestCaseId} was not found in {testDataPath}.");
        }

        private WinGetRegressionTestCase LoadTestData()
        {
            return LoadTestData(RegressionTestCaseId);
        }

        private static void ValidateTestData(WinGetRegressionTestCase testData, AssignmentMode assignmentMode)
        {
            if (!testData.Enabled)
            {
                throw new InvalidOperationException($"Test case {testData.TestCaseId} is disabled in regression data.");
            }

            if (!string.Equals(testData.Parameters.AppType, "Microsoft store app (new)", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Unexpected app type '{testData.Parameters.AppType}' for {testData.TestCaseId}.");
            }

            if (string.IsNullOrWhiteSpace(testData.Parameters.SearchApp) ||
                string.IsNullOrWhiteSpace(testData.Parameters.DisplayName) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Description) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Publisher) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Assignments.InstallContext) ||
                string.IsNullOrWhiteSpace(testData.Parameters.Assignments.SelectGroups))
            {
                throw new InvalidOperationException($"Test data is incomplete for {testData.TestCaseId}.");
            }
        }

        private static AssignmentMode GetAssignmentMode(WinGetRegressionAssignments assignments)
        {
            var modes = new List<AssignmentMode>();

            if (!string.IsNullOrWhiteSpace(assignments.Required))
            {
                modes.Add(AssignmentMode.Required);
            }

            if (!string.IsNullOrWhiteSpace(assignments.AvailableForEnrolledDevices))
            {
                modes.Add(AssignmentMode.Available);
            }

            if (!string.IsNullOrWhiteSpace(assignments.Uninstall))
            {
                modes.Add(AssignmentMode.Uninstall);
            }

            return modes.Count switch
            {
                1 => modes[0],
                0 => throw new InvalidOperationException($"No assignment mode configured for {assignments.SelectGroups}."),
                _ => throw new InvalidOperationException($"Multiple assignment modes are configured for {assignments.SelectGroups}.")
            };
        }

        private static string GetAssignmentBehaviorName(AssignmentMode assignmentMode)
        {
            return assignmentMode switch
            {
                AssignmentMode.Required => "Required",
                AssignmentMode.Available => "Available for enrolled devices",
                AssignmentMode.Uninstall => "Uninstall",
                _ => throw new ArgumentOutOfRangeException(nameof(assignmentMode), assignmentMode, null)
            };
        }

        private enum AssignmentMode
        {
            Required,
            Available,
            Uninstall
        }

        private sealed class WinGetRegressionRoot
        {
            [JsonPropertyName("testCases")]
            public List<WinGetRegressionTestCase> TestCases { get; set; } = new();
        }

        protected sealed class WinGetRegressionTestCase
        {
            [JsonPropertyName("testCaseId")]
            public string TestCaseId { get; set; } = string.Empty;

            [JsonPropertyName("enabled")]
            public bool Enabled { get; set; }

            [JsonPropertyName("parameters")]
            public WinGetRegressionParameters Parameters { get; set; } = new();
        }

        protected sealed class WinGetRegressionParameters
        {
            [JsonPropertyName("AppType")]
            public string AppType { get; set; } = string.Empty;

            [JsonPropertyName("search app")]
            public string SearchApp { get; set; } = string.Empty;

            [JsonPropertyName("displayName")]
            public string DisplayName { get; set; } = string.Empty;

            [JsonPropertyName("description")]
            public string Description { get; set; } = string.Empty;

            [JsonPropertyName("publisher")]
            public string Publisher { get; set; } = string.Empty;

            [JsonPropertyName("Assignments")]
            public WinGetRegressionAssignments Assignments { get; set; } = new();

            [JsonPropertyName("Device Validation")]
            public WinGetRegressionDeviceValidation DeviceValidation { get; set; } = new();
        }

        protected sealed class WinGetRegressionAssignments
        {
            [JsonPropertyName("Required")]
            public string Required { get; set; } = string.Empty;

            [JsonPropertyName("Available for enrolled devices")]
            public string AvailableForEnrolledDevices { get; set; } = string.Empty;

            [JsonPropertyName("uninstall")]
            public string Uninstall { get; set; } = string.Empty;

            [JsonPropertyName("select groups")]
            public string SelectGroups { get; set; } = string.Empty;

            [JsonPropertyName("Install context")]
            public string InstallContext { get; set; } = string.Empty;
        }

        protected sealed class WinGetRegressionDeviceValidation
        {
            [JsonPropertyName("App Installation Validation")]
            public string AppInstallationValidation { get; set; } = string.Empty;
        }
    }
}
