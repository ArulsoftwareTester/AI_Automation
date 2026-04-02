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
    /// Data-Driven Endpoint Security Firewall Tests
    /// Reads test data from TestData/EndpointSecurityFirewall_TestData.json
    /// Supports dropdown, multi-checkbox, and toggle+numeric setting types
    /// </summary>
    [TestFixture]
    public class DataDrivenEndpointSecurityFirewallTests : SecurityBaseline
    {
        protected ExtentTest? _test;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Environment.SetEnvironmentVariable("PLAYWRIGHT_CHROMIUM_ARGS",
                "--disable-dev-shm-usage --disable-gpu --no-sandbox --disable-setuid-sandbox --disable-web-security --disable-features=IsolateOrigins,site-per-process --js-flags=--max-old-space-size=512");
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoSH_09.pfx"));

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
            if (Page != null)
            {
                Page.SetDefaultTimeout(60000);
                Page.SetDefaultNavigationTimeout(60000);
            }
        }

        /// <summary>
        /// Returns all enabled test case IDs from EndpointSecurityFirewall_TestData.json
        /// </summary>
        public static IEnumerable<string> GetTestCases()
        {
            return TestDataLoader.GetEndpointSecurityFirewallTestData().TestCases
                .Where(tc => tc.Enabled)
                .Select(tc => tc.TestCaseId);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public async Task ExecuteEndpointSecurityFirewallTest(string testCaseId)
        {
            var testData = TestDataLoader.GetEndpointSecurityFirewallTestData()
                .TestCases.FirstOrDefault(t => t.TestCaseId == testCaseId);

            Assert.That(testData, Is.Not.Null, $"Test case {testCaseId} not found in EndpointSecurityFirewall_TestData.json");

            Console.WriteLine($"\n═══════════════════════════════════════════════════════════════");
            Console.WriteLine($"  EXECUTING FIREWALL TEST: {testData!.TestCaseId}");
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

                Console.WriteLine("\n[STEP 2] Creating Endpoint Security Firewall policy");
                var p = testData.Parameters;
                _test.Log(Status.Info, $"→ First Link:  {p.FirstLink}");
                _test.Log(Status.Info, $"→ Second Link: {p.SecondLink}");
                _test.Log(Status.Info, $"→ Platform:    {p.Platform}");
                _test.Log(Status.Info, $"→ Profile:     {p.Profile}");

                if (!string.IsNullOrEmpty(p.SettingsValue))
                {
                    if (p.ListValues?.Count > 0)
                        _test.Log(Status.Info, $"→ Setting: {p.SettingsValue} = [{string.Join(", ", p.ListValues)}] (list-add)");
                    else if (p.CheckboxValues?.Count > 0)
                        _test.Log(Status.Info, $"→ Setting: {p.SettingsValue} = [{string.Join(", ", p.CheckboxValues)}] (checkbox)");
                    else if (!string.IsNullOrEmpty(p.NumericValue))
                        _test.Log(Status.Info, $"→ Setting: {p.SettingsValue} = {p.NumericValue} (numeric)");
                    else
                        _test.Log(Status.Info, $"→ Setting: {p.SettingsValue} = {p.DropDownValue}");
                }

                await CreateEndPointSecurityPolicy(
                    Page,
                    p.FirstLink,
                    p.SecondLink,
                    p.Platform,
                    p.Profile,
                    p.SettingsValue,
                    p.DropDownValue,
                    p.NumericValue,
                    p.CheckboxValues,
                    p.AdditionalSettings,
                    p.ListValues
                );

                var screenshotPath = await ExtentReportHelper.CaptureScreenshot(Page, $"{testData.TestCaseId}_Success");
                if (!string.IsNullOrEmpty(screenshotPath))
                {
                    _test.Log(Status.Pass, "✅ TEST PASSED: Policy created successfully",
                        MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }

                Console.WriteLine($"\n✅ FIREWALL TEST PASSED: {testData.TestCaseId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ FIREWALL TEST FAILED: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                _test.Log(Status.Fail, $"❌ Test Failed: {ex.Message}");

                try
                {
                    var screenshotPath = await ExtentReportHelper.CaptureScreenshot(Page, $"{testData.TestCaseId}_Failed");
                    if (!string.IsNullOrEmpty(screenshotPath))
                        _test.AddScreenCaptureFromPath(screenshotPath);
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

            try
            {
                if (Context != null)
                    await Context.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Context cleanup warning: {ex.Message}");
            }
        }
    }
}
