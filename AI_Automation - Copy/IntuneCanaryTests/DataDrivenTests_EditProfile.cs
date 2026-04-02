using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using IntuneCanaryTests.Models;
using IntuneCanaryTests.Utilities;

namespace IntuneCanaryTests
{
    /// <summary>
    /// Data-Driven Tests for EditNewCreatedPolicy_TestData.json
    /// Executes tests in 3 parallel batches with separate browser contexts
    /// Add tests to TestData/EditNewCreatedPolicy_TestData.json - auto-discovered
    /// 
    /// CUSTOM JSON FILE SUPPORT:
    /// To use a custom JSON file, set EDIT_PROFILE_JSON_PATH in .env file:
    ///   EDIT_PROFILE_JSON_PATH = TestData\MyCustomEditTests.json (relative path)
    ///   EDIT_PROFILE_JSON_PATH = C:\MyTests\CustomTests.json (absolute path)
    /// 
    /// Custom JSON file must follow the same structure as EditNewCreatedPolicy_TestData.json
    /// </summary>
    public abstract class EditProfileTestBase : SecurityBaseline
    {
        protected ExtentTest? _test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Load .env file and display which JSON file will be used
            var envPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ".env"));
            if (File.Exists(envPath))
            {
                DotNetEnv.Env.Load(envPath);
            }
            
            var customFilePath = Environment.GetEnvironmentVariable("EDIT_PROFILE_JSON_PATH");
            if (!string.IsNullOrEmpty(customFilePath))
            {
                var resolvedPath = Path.IsPathRooted(customFilePath) 
                    ? customFilePath 
                    : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", customFilePath));
                    
                Console.WriteLine($"═══════════════════════════════════════════════════════════════");
                Console.WriteLine($"  USING CUSTOM EDIT PROFILE TEST DATA FILE");
                Console.WriteLine($"  Path: {resolvedPath}");
                Console.WriteLine($"═══════════════════════════════════════════════════════════════\n");
            }
            else
            {
                Console.WriteLine($"Using default test data file: TestData\\EditNewCreatedPolicy_TestData.json");
            }

            var createdPolicyListPath = Environment.GetEnvironmentVariable("CREATED_POLICY_LIST_JSON_PATH");
            if (!string.IsNullOrEmpty(createdPolicyListPath))
            {
                var rawPaths = createdPolicyListPath.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                Console.WriteLine($"═══════════════════════════════════════════════════════════════");
                Console.WriteLine($"  USING CUSTOM CREATED POLICY LIST JSON FILE(S)");
                foreach (var rawPath in rawPaths)
                {
                    var resolvedPath = Path.IsPathRooted(rawPath)
                        ? rawPath
                        : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", rawPath));
                    Console.WriteLine($"  Path: {resolvedPath}");
                }
                Console.WriteLine($"═══════════════════════════════════════════════════════════════\n");
            }
            else
            {
                Console.WriteLine($"PolicyName lookup: Auto-searching CreateNewPolicy\\NewPolicyList_*.json files (latest first)\n");
            }
            
            // Configure Playwright browser launch args for resource optimization
            Environment.SetEnvironmentVariable("PLAYWRIGHT_CHROMIUM_ARGS", 
                "--disable-dev-shm-usage --disable-gpu --no-sandbox --disable-setuid-sandbox --disable-web-security --disable-features=IsolateOrigins,site-per-process --js-flags=--max-old-space-size=512");
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
            
            return new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
                IgnoreHTTPSErrors = true,
                ClientCertificates = new[]
                {
                    new ClientCertificate
                    {
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
            Console.WriteLine($"Test Data - EditProfile: {TestDataLoader.GetDataSummary()}");
            
            // Set Playwright timeout to 30 seconds for Login and EditProfile operations
            // Element-level timeouts use VERY_SHORT_TIMEOUT (10s), SHORT_TIMEOUT (30s), or LONG_TIMEOUT (120s) as needed
            if (Page != null)
            {
                Page.SetDefaultTimeout(30000);
                Page.SetDefaultNavigationTimeout(30000);
            }
        }

        protected async Task ExecuteEditNewCreatedPolicyTest(string testCaseId)
        {
            var testData = TestDataLoader.GetEditProfileData()
                .TestCases.FirstOrDefault(t => t.TestCaseId == testCaseId);

            Assert.That(testData, Is.Not.Null, $"Test case {testCaseId} not found in EditNewCreatedPolicy_TestData.json");

            Console.WriteLine($"\n═══════════════════════════════════════════════════════════════");
            Console.WriteLine($"  EXECUTING: {testData!.TestCaseId}");
            Console.WriteLine($"═══════════════════════════════════════════════════════════════\n");
            Console.WriteLine($"Test Case: {testData.TestName}");
            Console.WriteLine($"Description: {testData.Description}");
            Console.WriteLine($"Priority: {testData.Priority}\n");

            _test = TestInitialize.CreateTest($"{testData.TestCaseId}: {testData.TestName}", testData.Description);
            _test.AssignCategory(testData.Category);

            try
            {
                Assert.That(Page, Is.Not.Null, "Page object is null");

                Console.WriteLine("[STEP 1] Logging into Intune Portal");
                _test.Log(Status.Info, "Starting Intune Login");
                await Login(Page);
                await Task.Delay(3000);
                _test.Log(Status.Pass, "Successfully logged into Intune Portal");

                Console.WriteLine("\n[STEP 2] Navigating to Intune home page");
                var tcEnvEdit = Environment.GetEnvironmentVariable("TC_Env");
                var intuneUrlEdit = (tcEnvEdit?.Equals("SH", StringComparison.OrdinalIgnoreCase) == true)
                    ? (Environment.GetEnvironmentVariable("INTUNE_SH_URL") ?? "https://aka.ms/IntuneSH")
                    : (Environment.GetEnvironmentVariable("INTUNE_PE_URL") ?? "https://aka.ms/intune");
                await Page.GotoAsync(intuneUrlEdit);
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                await Task.Delay(2000);
                _test.Log(Status.Pass, "Home page loaded successfully");

                Console.WriteLine("\n[STEP 3] Looking up PolicyName from CreateNewPolicy records");
                // Lookup the actual PolicyName created for this testCaseId from NewPolicyList JSON
                var policyNameToEdit = TestDataLoader.GetPolicyNameOrDefault(testCaseId, "");
                _test.Log(Status.Info, $"PolicyName to edit: {policyNameToEdit}");
                
                Console.WriteLine($"→ PolicyName: {policyNameToEdit}");

                Console.WriteLine("\n[STEP 4] Editing policy");
                _test.Log(Status.Info, $"Editing policy: {policyNameToEdit}");
                _test.Log(Status.Info, $"→ Changing: {testData.Parameters.ParentDropDown} to {testData.Parameters.ParentDropDownOption}");
                if (!string.IsNullOrEmpty(testData.Parameters.ChildDropDown))
                {
                    _test.Log(Status.Info, $"→ Child Setting: {testData.Parameters.ChildDropDown} = {testData.Parameters.ChildDropDownOption}");
                }
                
                var p = testData.Parameters;
                await editNewCreatedPolicy(
                    Page,
                    p.FirstLink,
                    p.SecondLink,
                    p.SecBaselineName,        // Keep secBaselineName for clicking baseline type
                    p.ConfigurationSettings,
                    p.ParentDropDown,
                    p.ParentDropDownOption,
                    p.ChildDropDown,
                    p.ChildDropDownOption,
                    p.ParentSectionPath,
                    p.ChildSectionPath,
                    policyNameToEdit          // Pass looked-up PolicyName as the policyName parameter
                );

                var screenshotPath = await ExtentReportHelper.CaptureScreenshot(Page, $"{testData.TestCaseId}_Success");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    _test.Log(Status.Pass, "✅ TEST PASSED: Policy edited successfully", 
                        MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }

                Console.WriteLine($"\n✅ TEST PASSED: {testData.TestCaseId} executed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ TEST FAILED: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                _test.Log(Status.Fail, $"❌ Test Failed: {ex.Message}");

                try
                {
                    var screenshotPath = await ExtentReportHelper.CaptureScreenshot(Page, $"{testData.TestCaseId}_Failed");
                    if (!string.IsNullOrEmpty(screenshotPath))
                    {
                        _test.AddScreenCaptureFromPath(screenshotPath);
                    }
                }
                catch (Exception screenshotEx)
                {
                    Console.WriteLine($"Failed to capture screenshot: {screenshotEx.Message}");
                    _test.Log(Status.Warning, $"Could not capture failure screenshot: {screenshotEx.Message}");
                }

                throw;
            }
        }

        [TearDown]
        public async Task TestCleanup()
        {
            Console.WriteLine("\n[TEARDOWN] Starting test cleanup...");
            ExtentReportHelper.FlushReport();
            
            // Explicit cleanup to free browser resources
            try
            {
                if (Context != null)
                {
                    await Context.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Context cleanup warning: {ex.Message}");
            }
        }
    }

    #region EditProfile Parallel Batches

    [TestFixture]
    public class EditProfile_Batch1 : EditProfileTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetEditProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Take(batchSize).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteEditNewCreatedPolicyTest(testCaseId);
    }

    [TestFixture]
    public class EditProfile_Batch2 : EditProfileTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetEditProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Skip(batchSize).Take(batchSize).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteEditNewCreatedPolicyTest(testCaseId);
    }

    [TestFixture]
    public class EditProfile_Batch3 : EditProfileTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetEditProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Skip(batchSize * 2).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteEditNewCreatedPolicyTest(testCaseId);
    }

    #endregion
}
