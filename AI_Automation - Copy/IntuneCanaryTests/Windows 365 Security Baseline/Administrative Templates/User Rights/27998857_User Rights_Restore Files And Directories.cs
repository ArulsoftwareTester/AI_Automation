using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27998857_User_Rights_Restore_Files_And_Directories : PageTest
    {
        private ExtentTest? _test;
        
        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "User Rights - Restore Files And Directories");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 27998857");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_27998857_User_Rights_Restore_Files_And_Directories()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_27998857");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                                                _test?.Info("Step 2: Creating NEW Profile - Config 1");
                await securityBaseline.CreateNewProfile(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Windows 365 Security Baseline",
                    "User Rights",
                    "Restore Files And Directories",
                    "Configured",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully created NEW Profile with Config 1: Configured");

                _test?.Info("Step 3: Editing Profile - Config 2");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Windows 365 Security Baseline",
                    "User Rights",
                    "Restore Files And Directories",
                    "Not configured",
                    "On",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile with Config 2: Not configured with Toggle ON");

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_27998857");
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
            TestInitialize.LogTestResult(TestContext.CurrentContext);
            _test?.Info($"Test ended at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
    }
}
