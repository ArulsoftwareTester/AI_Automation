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
    public class T28012810_LetAppsActivateWithVoiceAboveLock : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Privacy - Let Apps Activate With Voice Above Lock");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 28012810");
            _test.Info("Category: Privacy");
        }

        [Test]
        public async Task Test_28012810_LetAppsActivateWithVoiceAboveLock()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_28012810");
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
                    "Privacy",
                    "Let Apps Activate With Voice Above Lock",
                    "User in control. Users can decide if Windows apps can be activated by voice while the screen is locked using Settings > Privacy options on the device.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully created NEW Profile with Config 1: User in control. Users can decide if Windows apps can be activated by voice while the screen is locked using Settings > Privacy options on the device.");

                _test?.Info("Step 3: Editing Profile - Config 2");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Windows 365 Security Baseline",
                    "Privacy",
                    "Let Apps Activate With Voice Above Lock",
                    "Force allow. Windows apps can be activated by voice while the screen is locked, and users cannot change it.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile with Config 2: Force allow. Windows apps can be activated by voice while the screen is locked, and users cannot change it.");
                _test?.Info("Step 4: Editing Profile - Config 3");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Windows 365 Security Baseline",
                    "Privacy",
                    "Let Apps Activate With Voice Above Lock",
                    "Force deny. Windows apps cannot be activated by voice while the screen is locked, and users cannot change it.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile with Config 3: Force deny. Windows apps cannot be activated by voice while the screen is locked, and users cannot change it.");

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_28012810");
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
