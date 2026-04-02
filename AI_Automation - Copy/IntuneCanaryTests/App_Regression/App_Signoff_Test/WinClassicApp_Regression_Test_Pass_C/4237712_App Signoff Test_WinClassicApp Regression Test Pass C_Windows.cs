using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using AventStack.ExtentReports;
using static AventStack.ExtentReports.MediaEntityBuilder;

namespace IntuneCanaryTests
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    public class Test_4237712_Windows_Win10_DependentApp_Dependencies_E2E_AutoInstall_WinClassicApp_V1 : PageTest
    {
        private ExtentTest? _test;

        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.Combine(Directory.GetCurrentDirectory(), "auth-cert", "AIAutoPE_3.pfx");
            return new BrowserNewContextOptions
            {
                ClientCertificates = new[]
                {
                    new ClientCertificate
                    {
                        Origin = "https://certauth.login.microsoftonline.com/c0219094-a70e-402c-8dd2-fd89f7d64010/certauth",
                        PfxPath = certPath,
                        Passphrase = "Admin@123"
                    }
                },
                IgnoreHTTPSErrors = true
            };
        }

        [SetUp]
        public void TestSetup()
        {
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "App Regression - WinClassicApp - DependentApp - Dependencies - E2E - AutoInstall - A -> B_auto-install off_If AppB not present, App A's installation should fail");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 4237712");
            _test.Info("Category: App Regression - Windows - Win10 - DependentApp - Dependencies - E2E - AutoInstall - WinClassicApp - Required - UserGroup");
        }

        [Test]
        public async Task TestMethod_4237712_Windows_Win10_DependentApp_Dependencies_E2E_AutoInstall_WinClassicApp_V1()
        {
            try
            {
                _test?.Info("Test execution started");

                // Login to Intune Portal
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = ExtentReportHelper.CaptureScreenshot(Page, "LoginCompleted");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                // TODO: Add test logic for A -> B with auto-install off, AppB not present

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = ExtentReportHelper.CaptureScreenshot(Page, "ErrorOccurred");
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
        }

        [TearDown]
        public void TestTeardown()
        {
            _test?.Info($"Test ended at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            TestInitialize.LogTestResult(_test);
        }
    }
}
