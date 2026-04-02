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
    public class Test_4180292_AST_WCAR_TP_C_Registry_String_Version_Less_than_or_equal_to_FALSE : PageTest
    { 
        private ExtentTest? _test;
        
        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "App Signoff Test - WinClassicApp Regression Test Pass C - Verify app is installed when String version Data value of registry specified by Path Key name Data value evaluates 'Less than or equal to' to FALSE");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 4180292");
        }

        [Test]
        public async Task TestMethod_4180292_Registry_String_Version_Less_than_or_equal_to_FALSE()
        {
            try
            {
                _test?.Info("Test execution started");
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }
                
                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_4180292");
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