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
    public class T21621393_Microsoft_Outlook_2016_Outlook_Security_Mode_User : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Windows 365 Security Baseline - Microsoft Outlook 2016 Outlook Security Mode User Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21621393");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_21621393_Microsoft_Outlook_2016_Outlook_Security_Mode_User()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_21621393");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                _test?.Info("Step 2: Creating Windows 365 Security Baseline Profile with Administrative Templates");
                await securityBaseline.createProfileAdminTemplate(Page, "Windows 365", "Enabled");
                _test?.Pass("Successfully created Windows 365 Security Baseline Profile with Administrative Templates");

                _test?.Info("Step 3: Triggering MDM Policy Sync");
                await securityBaseline.MDMPolicySync(Page);
                _test?.Pass("Successfully completed MDM Policy Sync");

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21621393");
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
