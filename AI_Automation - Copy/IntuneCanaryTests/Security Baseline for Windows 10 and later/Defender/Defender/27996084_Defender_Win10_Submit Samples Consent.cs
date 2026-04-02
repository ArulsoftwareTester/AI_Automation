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
    public class Defender_27996084_Win10_SubmitSamplesConsent : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Security Baseline for Windows 10 and later - Defender: Submit Samples Consent");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 27996084");
            _test.Info("Category: Security Baseline for Windows 10 and later - Defender");
        }

        [Test]
        public async Task Test_27996084_Defender_Win10_SubmitSamplesConsent()
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
                
                _test?.Info("Step 2: Creating NEW Profile for Defender - Submit Samples Consent");
                await securityBaseline.CreateNewProfile(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Defender",
                    "Submit Samples Consent",
                    "Always prompt",
                    "",
                    "",
                    "",
                    ""
                );    
                _test?.Pass("Successfully created NEW Profile for Submit Samples Consent"); 

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created_27996084");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }

                _test?.Info("Step 3: Editing Profile - Changing Submit Samples Consent to Send safe samples automatically");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Defender",
                    "Submit Samples Consent",
                    "Send safe samples automatically",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - Submit Samples Consent set to Send safe samples automatically");

                var editScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_27996084");
                if (!string.IsNullOrEmpty(editScreenshot))
                {
                    _test?.Pass("Profile edit completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot).Build());
                }

                _test?.Info("Step 4: Editing Profile - Changing Submit Samples Consent to Send all samples automatically");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Defender",
                    "Submit Samples Consent",
                    "Never send",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - Submit Samples Consent set to Send all samples automatically");

                var editScreenshot2 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_27996084_2");
                if (!string.IsNullOrEmpty(editScreenshot2))
                {
                    _test?.Pass("Profile edit 2 completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot2).Build());
                }

                _test?.Info("Step 5: Editing Profile - Changing Submit Samples Consent to Never send");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Defender",
                    "Submit Samples Consent",
                    "Send all samples automatically",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - Submit Samples Consent set to Never send");

                var editScreenshot3 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_27996084_3");
                if (!string.IsNullOrEmpty(editScreenshot3))
                {
                    _test?.Pass("Profile edit 3 completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot3).Build());
                }

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_27996084");
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
