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
    public class SecurityZonesDoNotAllowUsersToAddDeleteSites : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Security Baseline for Windows 10 and later - Administrative Templates - Internet Explorer - Security Zones Do not allow users to add delete sites Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 28037340");
        }
        [Test]
        public async Task Test_28037340_AdministrativeTemplatesInternetExplorerSecurityZonesDoNotAllowUsersToAddDeleteSites()
        {
            try
            {
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");
                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_28037340");
                if (!string.IsNullOrEmpty(loginScreenshot)) _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later", "Enabled");
                _test?.Pass("Successfully created Windows 10 Security Baseline Profile");
                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created_28037340");
                if (!string.IsNullOrEmpty(profileScreenshot)) _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                await securityBaseline.MDMPolicySync(Page);
                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_28037340");
                if (!string.IsNullOrEmpty(errorScreenshot)) _test?.Fail($"Test failed: {ex.Message}", MediaEntityBuilder.CreateScreenCaptureFromPath(errorScreenshot).Build());
                else _test?.Fail($"Test failed: {ex.Message}");
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
