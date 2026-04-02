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
    /// Data-Driven Tests for CreateNewProfile_TestData.json
    /// Executes tests in 4 parallel batches with separate browser contexts
    /// Add tests to TestData/CreateNewProfile_TestData.json - auto-discovered
    /// Enhanced with periodic resource cleanup to prevent exhaustion
    /// 
    /// CUSTOM JSON FILE SUPPORT:
    /// To use a custom JSON file, set CREATE_PROFILE_JSON_PATH in .env file:
    ///   CREATE_PROFILE_JSON_PATH = TestData\Win_10_Create_1.json (relative path)
    ///   CREATE_PROFILE_JSON_PATH = C:\MyTests\CustomTests.json (absolute path)
    /// 
    /// Custom JSON file must follow the same structure as CreateNewProfile_TestData.json
    /// </summary>
    public abstract class CreateProfileTestBase : SecurityBaseline
    {
        protected ExtentTest? _test;
        private static int _testCount = 0;
        private static readonly object _cleanupLock = new object();

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Kill any orphaned browser processes before starting tests
            CleanupBrowserProcesses();
            
            // Load .env file and display which JSON file will be used
            var envPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", ".env"));
            if (File.Exists(envPath))
            {
                DotNetEnv.Env.Load(envPath);
            }
            
            var customFilePath = Environment.GetEnvironmentVariable("CREATE_PROFILE_JSON_PATH");
            if (!string.IsNullOrEmpty(customFilePath))
            {
                var resolvedPath = Path.IsPathRooted(customFilePath) 
                    ? customFilePath 
                    : Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", customFilePath));
                    
                Console.WriteLine($"═══════════════════════════════════════════════════════════════");
                Console.WriteLine($"  USING CUSTOM CREATE PROFILE TEST DATA FILE");
                Console.WriteLine($"  Path: {resolvedPath}");
                Console.WriteLine($"═══════════════════════════════════════════════════════════════\n");
            }
            else
            {
                Console.WriteLine($"Using default test data file: TestData\\CreateNewProfile_TestData.json");
            }
            
            // Configure Playwright browser launch args for resource optimization
            Environment.SetEnvironmentVariable("PLAYWRIGHT_CHROMIUM_ARGS", 
                "--disable-dev-shm-usage --disable-gpu --no-sandbox --disable-setuid-sandbox --disable-web-security --disable-features=IsolateOrigins,site-per-process --js-flags=--max-old-space-size=4096");
        }

        private void CleanupBrowserProcesses()
        {
            try
            {
                var processes = System.Diagnostics.Process.GetProcesses()
                    .Where(p => p.ProcessName.Contains("chrome", StringComparison.OrdinalIgnoreCase) ||
                               p.ProcessName.Contains("msedge", StringComparison.OrdinalIgnoreCase) ||
                               p.ProcessName.Contains("playwright", StringComparison.OrdinalIgnoreCase));
                
                foreach (var process in processes)
                {
                    try
                    {
                        process.Kill(true);
                        process.WaitForExit(5000);
                    }
                    catch { /* Ignore if process already exited */ }
                }
                
                Console.WriteLine("Cleaned up orphaned browser processes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Browser cleanup warning: {ex.Message}");
            }
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
            Console.WriteLine($"Test Data - CreateProfile: {TestDataLoader.GetDataSummary()}");
            
            // Set Playwright timeout to 30 seconds for Login and CreateNewProfile operations
            // Element-level timeouts use VERY_SHORT_TIMEOUT (10s), SHORT_TIMEOUT (30s), or LONG_TIMEOUT (120s) as needed
            if (Page != null)
            {
                Page.SetDefaultTimeout(30000);
                Page.SetDefaultNavigationTimeout(30000);
            }
        }

        protected async Task ExecuteCreateNewProfileTest(string testCaseId)
        {
            var testData = TestDataLoader.GetCreateProfileData()
                .TestCases.FirstOrDefault(t => t.TestCaseId == testCaseId);

            Assert.That(testData, Is.Not.Null, $"Test case {testCaseId} not found in CreateNewProfile_TestData.json");

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

                Console.WriteLine("\n[STEP 2] Creating new security baseline profile");
                _test.Log(Status.Info, $"Creating profile: {testData.Parameters.SecBaselineName}");
                _test.Log(Status.Info, $"→ Parent Setting: {testData.Parameters.ParentDropDown} = {testData.Parameters.ParentDropDownOption}");
                if (!string.IsNullOrEmpty(testData.Parameters.ChildDropDown))
                {
                    _test.Log(Status.Info, $"→ Child Setting: {testData.Parameters.ChildDropDown} = {testData.Parameters.ChildDropDownOption}");
                }
                
                var p = testData.Parameters;
                await CreateNewProfile(
                    Page,
                    p.FirstLink,
                    p.SecondLink,
                    p.SecBaselineName,
                    p.ConfigurationSettings,
                    p.ParentDropDown,
                    p.ParentDropDownOption,
                    p.ChildDropDown,
                    p.ChildDropDownOption,
                    p.ParentSectionPath,
                    p.ChildSectionPath
                );

                var screenshotPath = await ExtentReportHelper.CaptureScreenshot(Page, $"{testData.TestCaseId}_Success");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    _test.Log(Status.Pass, "✅ TEST PASSED: Profile created successfully", 
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
            
            // Periodic aggressive cleanup every 20 tests to prevent resource buildup
            lock (_cleanupLock)
            {
                _testCount++;
                if (_testCount % 20 == 0)
                {
                    Console.WriteLine($"[PERIODIC CLEANUP] After {_testCount} tests - killing orphaned browsers");
                    CleanupBrowserProcesses();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeCleanup()
        {
            // Final cleanup of any remaining browser processes
            CleanupBrowserProcesses();
        }
    }

    #region CreateProfile Parallel Batches

    [TestFixture]
    public class CreateProfile_Batch1 : CreateProfileTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetCreateProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            Console.WriteLine($"[CreateProfile_Batch1] Total enabled tests: {allTests.Count}");
            int batchSize = (int)Math.Ceiling(allTests.Count / 4.0);
            Console.WriteLine($"[CreateProfile_Batch1] Batch size: {batchSize}");
            var result = allTests.Take(batchSize).Select(tc => tc.TestCaseId).ToList();
            Console.WriteLine($"[CreateProfile_Batch1] Returning {result.Count} test IDs");
            Console.WriteLine($"[CreateProfile_Batch1] First: {result.First()}, Last: {result.Last()}");
            return result;
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteCreateNewProfileTest(testCaseId);
    }

    [TestFixture]
    public class CreateProfile_Batch2 : CreateProfileTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetCreateProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 4.0);
            return allTests.Skip(batchSize).Take(batchSize).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteCreateNewProfileTest(testCaseId);
    }

    [TestFixture]
    public class CreateProfile_Batch3 : CreateProfileTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetCreateProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 4.0);
            return allTests.Skip(batchSize * 2).Take(batchSize).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteCreateNewProfileTest(testCaseId);
    }

    [TestFixture]
    public class CreateProfile_Batch4 : CreateProfileTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetCreateProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 4.0);
            return allTests.Skip(batchSize * 3).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteCreateNewProfileTest(testCaseId);
    }

    #endregion
}
