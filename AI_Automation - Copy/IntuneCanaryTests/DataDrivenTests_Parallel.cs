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
    /// Base class for parallel data-driven tests
    /// Each test case gets its own fixture for true parallel execution
    /// </summary>
    public abstract class ParallelDataDrivenTestBase : SecurityBaseline
    {
        protected ExtentTest? _test;

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
            Console.WriteLine($"Test Data Summary - {TestDataLoader.GetDataSummary()}");
        }

        protected async Task ExecuteCreateNewProfileTest(string testCaseId)
        {
            var testData = TestDataLoader.GetCreateProfileData()
                .TestCases.FirstOrDefault(t => t.TestCaseId == testCaseId);

            Assert.That(testData, Is.Not.Null, $"Test case {testCaseId} not found in test data.");

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
        public void TestCleanup()
        {
            Console.WriteLine("\n[TEARDOWN] Starting test cleanup...");
            ExtentReportHelper.FlushReport();
        }

        protected async Task ExecuteEditNewCreatedPolicyTest(string testCaseId)
        {
            var testData = TestDataLoader.GetEditProfileData()
                .TestCases.FirstOrDefault(t => t.TestCaseId == testCaseId);

            Assert.That(testData, Is.Not.Null, $"Test case {testCaseId} not found in test data.");

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
                var tcEnvParallel = Environment.GetEnvironmentVariable("TC_Env");
                var intuneUrlParallel = (tcEnvParallel?.Equals("SH", StringComparison.OrdinalIgnoreCase) == true)
                    ? (Environment.GetEnvironmentVariable("INTUNE_SH_URL") ?? "https://aka.ms/IntuneSH")
                    : (Environment.GetEnvironmentVariable("INTUNE_PE_URL") ?? "https://aka.ms/intune");
                await Page.GotoAsync(intuneUrlParallel);
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new PageWaitForLoadStateOptions { Timeout = 30000 });
                await Task.Delay(2000);
                _test.Log(Status.Pass, "Home page loaded successfully");

                Console.WriteLine("\n[STEP 3] Editing policy");
                _test.Log(Status.Info, $"Editing policy: {testData.Parameters.SecBaselineName}");
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

        protected async Task ExecuteVMSyncTest(string testCaseId)
        {
            var testData = TestDataLoader.GetVMSyncData()
                .TestCases.FirstOrDefault(t => t.TestCaseId == testCaseId);

            Assert.That(testData, Is.Not.Null, $"Test case {testCaseId} not found in test data.");

            Console.WriteLine($"\n═══════════════════════════════════════════════════════════════");
            Console.WriteLine($"  EXECUTING: {testData!.TestCaseId}");
            Console.WriteLine($"═══════════════════════════════════════════════════════════════\n");
            Console.WriteLine($"Test Case: {testData.TestName}");
            Console.WriteLine($"Description: {testData.Description}");
            Console.WriteLine($"Search Term: {testData.Parameters.SearchTerm}\n");

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

                Console.WriteLine($"\n[STEP 2] Executing VMSync validation");
                _test.Log(Status.Info, $"Validating policy in MDM report");
                _test.Log(Status.Info, $"→ Search Term: {testData.Parameters.SearchTerm}");
                
                await VMSync(Page, testData.Parameters.SearchTerm);

                var screenshotPath = await ExtentReportHelper.CaptureScreenshot(Page, $"{testData.TestCaseId}_Success");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    _test.Log(Status.Pass, "✅ TEST PASSED: VMSync validation completed successfully", 
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
    }

    #region CreateProfile Test Fixtures - Parallel Batches

    /// <summary>
    /// Batch 1 of CreateNewProfile tests for parallel execution
    /// Runs first 1/3 of tests from CreateNewProfile_TestData.json
    /// </summary>
    [TestFixture]
    public class CreateProfile_Batch1 : ParallelDataDrivenTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetCreateProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Take(batchSize).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteCreateNewProfileTest(testCaseId);
    }

    /// <summary>
    /// Batch 2 of CreateNewProfile tests for parallel execution
    /// Runs middle 1/3 of tests from CreateNewProfile_TestData.json
    /// </summary>
    [TestFixture]
    public class CreateProfile_Batch2 : ParallelDataDrivenTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetCreateProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Skip(batchSize).Take(batchSize).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteCreateNewProfileTest(testCaseId);
    }

    /// <summary>
    /// Batch 3 of CreateNewProfile tests for parallel execution
    /// Runs last 1/3 of tests from CreateNewProfile_TestData.json
    /// </summary>
    [TestFixture]
    public class CreateProfile_Batch3 : ParallelDataDrivenTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetCreateProfileData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Skip(batchSize * 2).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteCreateNewProfileTest(testCaseId);
    }

    #endregion

    #region EditProfile Test Fixtures - Parallel Batches

    [TestFixture]
    public class EditProfile_Batch1 : ParallelDataDrivenTestBase
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
    public class EditProfile_Batch2 : ParallelDataDrivenTestBase
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
    public class EditProfile_Batch3 : ParallelDataDrivenTestBase
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

    #region VMSync Test Fixtures - Parallel Batches

    [TestFixture]
    public class VMSync_Batch1 : ParallelDataDrivenTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetVMSyncData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Take(batchSize).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteVMSyncTest(testCaseId);
    }

    [TestFixture]
    public class VMSync_Batch2 : ParallelDataDrivenTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetVMSyncData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Skip(batchSize).Take(batchSize).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteVMSyncTest(testCaseId);
    }

    [TestFixture]
    public class VMSync_Batch3 : ParallelDataDrivenTestBase
    {
        public static IEnumerable<string> GetTestCases()
        {
            var allTests = TestDataLoader.GetVMSyncData().TestCases
                .Where(tc => tc.Enabled)
                .ToList();
            
            int batchSize = (int)Math.Ceiling(allTests.Count / 3.0);
            return allTests.Skip(batchSize * 2).Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task Execute(string testCaseId) => await ExecuteVMSyncTest(testCaseId);
    }

    #endregion
}
