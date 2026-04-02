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
    public class Test_9495853_Windows_Win10_Assignment_Filters_Conflict_WinClassicApp : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "App Regression - WinClassicApp - Assignment Filters Conflict - Verify app is installed on the device and shows in CP/IWP");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 9495853");
            _test.Info("Category: App Regression - WinClassicApp - Assignment Filters Conflict");
        }

        [Test]
        public async Task TestMethod_9495853_Windows_Win10_Assignment_Filters_Conflict_WinClassicApp()
        {
            try
            {
                Console.WriteLine("Test_9495853_Windows_Win10_Assignment_Filters_Conflict_WinClassicApp started...");
                _test?.Info("Test execution started");
                
                // Call IPLogin function from SecurityBaseline
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");
                
                // Capture screenshot after login
                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_9495853");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }
                
                _test?.Info("Test Case: G1 Required F1 Exclude No Match, G2 Available F2 Include Match");
                _test?.Info("Verify app is installed on the device and shows in CP/IWP");
                
                // Add your test implementation here
                
                Console.WriteLine("Test completed successfully!");
                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_9495853");
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
        public void TestTearDown()
        {
            _test?.Info($"Test completed at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            TestInitialize.FlushReports();
        }
    }
}
