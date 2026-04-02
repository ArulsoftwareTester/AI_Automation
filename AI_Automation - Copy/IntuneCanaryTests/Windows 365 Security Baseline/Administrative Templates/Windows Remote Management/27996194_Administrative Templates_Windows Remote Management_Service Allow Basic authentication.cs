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
    public class ServiceAllowBasicAuthentication : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Windows 365 Security baseline - Administrative Templates - Windows Remote Management - Service Allow Basic authentication Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 27996194");
            _test.Info("Category: Administrative Templates - Windows Remote Management");
        }


        [Test]
        public async Task Test_27996194_AdministrativeTemplatesWindowsRemoteManagementServiceAllowBasicAuthentication()
        {
            try
            {
                Console.WriteLine("Test_27996194_AdministrativeTemplatesWindowsRemoteManagementServiceAllowBasicAuthentication started...");
                _test?.Info("Test execution started");
                
                // Call IPLogin function from SecurityBaseline
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");
                
                // Capture screenshot after login
                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_27996194");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }
                
                // Call createProfile_Win365 function from SecurityBaseline
                _test?.Info("Step 2: Creating Windows 365 Security Baseline Profile");
                await securityBaseline.createProfile_Win365(Page, "Windows 365", "Enabled");
                _test?.Pass("Successfully created Windows 365 Security Baseline Profile");
                
                // Capture screenshot after profile creation
                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created_27996194");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }
                
                // Call MDMPolicySync function from SecurityBaseline
                _test?.Info("Step 3: Triggering MDM Policy Sync");
                await securityBaseline.MDMPolicySync(Page);
                _test?.Pass("Successfully completed MDM Policy Sync");
                
                Console.WriteLine("Test completed successfully!");
                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_27996194");
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
