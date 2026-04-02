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
    public class T29904546_PreventPeruserInstallationOfActivexControls : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Windows 365 Security Baseline - Prevent per-user installation of ActiveX controls Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 29904546");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_29904546_PreventPeruserInstallationOfActivexControls()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.Login(Page);
                _test?.Pass("Successfully logged into Intune Portal");
                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }
                _test?.Info("Step 2: Creating NEW Profile");
                await securityBaseline.CreateNewProfile(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Windows 365 Security Baseline",
                                                        "Administrative Templates",
                                                        "Prevent per-user installation of ActiveX controls",   // Parent dropdown name
                                                        "Enabled",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "Windows Components > Internet Explorer",  // Parent path
                                                        "Windows Components > Internet Explorer"   // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile"); 

                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Windows 365 Security Baseline",
                                                        "Administrative Templates",
                                                        "Prevent per-user installation of ActiveX controls",              // Parent dropdown name
                                                        "Disabled",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "Windows Components > Internet Explorer",  // Parent path
                                                        "Windows Components > Internet Explorer"   // Child path
                                    );
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Windows 365 Security Baseline",
                                                        "Administrative Templates",
                                                        "Prevent per-user installation of ActiveX controls",              // Parent dropdown name
                                                        "Not configured",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "Windows Components > Internet Explorer",  // Parent path
                                                        "Windows Components > Internet Explorer"   // Child path
                                    );

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }
                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_29904546");
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
