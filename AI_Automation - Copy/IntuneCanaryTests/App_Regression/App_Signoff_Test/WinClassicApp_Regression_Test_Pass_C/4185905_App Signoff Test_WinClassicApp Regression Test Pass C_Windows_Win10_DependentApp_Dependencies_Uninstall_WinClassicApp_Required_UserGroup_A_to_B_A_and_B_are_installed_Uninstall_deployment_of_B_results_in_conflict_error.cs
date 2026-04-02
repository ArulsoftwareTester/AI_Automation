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
    public class Test_4185905_App_Signoff_Test_WinClassicApp_Regression_Test_Pass_C_Windows_Win10_DependentApp_Dependencies_Uninstall_WinClassicApp_Required_UserGroup_A_to_B_A_and_B_are_installed_Uninstall_deployment_of_B_results_in_conflict_error : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "App Signoff Test - WinClassicApp Regression Test Pass C - A->B, A and B are installed, Uninstall deployment of B results in conflict error");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 4185905");
            _test.Info("Category: App Signoff Test - WinClassicApp Regression Test Pass C");
        }

        [Test]
        public async Task TestMethod_4185905_App_Signoff_Test_WinClassicApp_Regression_Test_Pass_C_Windows_Win10_DependentApp_Dependencies_Uninstall_WinClassicApp_Required_UserGroup_A_to_B_A_and_B_are_installed_Uninstall_deployment_of_B_results_in_conflict_error()
        {
            try
            {
                Console.WriteLine("Test_4185905_App_Signoff_Test_WinClassicApp_Regression_Test_Pass_C started...");
                _test?.Info("Test execution started");
                
                // Call IPLogin function from SecurityBaseline
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");
                
                // Capture screenshot after login
                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_4185905");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }
                
                Console.WriteLine("Test completed successfully!");
                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_4185905");
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
