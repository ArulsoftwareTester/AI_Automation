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
    public class Defender_27995902_Win10_BlockOfficeApplicationsFromCreatingExecutableContent : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Security Baseline for Windows 10 and later - Defender: Block Office applications from creating executable content");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 27995902");
            _test.Info("Category: Security Baseline for Windows 10 and later - Defender");
        }

        [Test]
        public async Task Test_27995902_Defender_Win10_BlockOfficeApplicationsFromCreatingExecutableContent()
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
                
                _test?.Info("Step 2: Creating NEW Profile for Defender - Block Office applications from creating executable content");
                await securityBaseline.CreateNewProfile(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Defender",
                    "Block Office applications from creating executable content",
                    "Off",
                    "",
                    "",
                    "",
                    ""
                );    
                _test?.Pass("Successfully created NEW Profile for Block Office applications from creating executable content"); 

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created_27995902");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }

                _test?.Info("Step 3: Editing Profile - Changing to Block");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Defender",
                    "Block Office applications from creating executable content",
                    "Block",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - set to Block");

                var editScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_Block_27995902");
                if (!string.IsNullOrEmpty(editScreenshot))
                {
                    _test?.Pass("Profile edit to Block completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot).Build());
                }

                _test?.Info("Step 4: Editing Profile - Changing to Audit");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Defender",
                    "Block Office applications from creating executable content",
                    "Audit",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - set to Audit");

                var editScreenshot2 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_Audit_27995902");
                if (!string.IsNullOrEmpty(editScreenshot2))
                {
                    _test?.Pass("Profile edit to Audit completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot2).Build());
                }

                _test?.Info("Step 5: Editing Profile - Changing to Warn");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Defender",
                    "Block Office applications from creating executable content",
                    "Warn",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - set to Warn");

                var editScreenshot3 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_Warn_27995902");
                if (!string.IsNullOrEmpty(editScreenshot3))
                {
                    _test?.Pass("Profile edit to Warn completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot3).Build());
                }

                // _test?.Info("Step 6: Editing Profile - Changing to Not configured");
                // await securityBaseline.editNewCreatedPolicy(
                //     Page,
                //     "Endpoint security",
                //     "Security baselines",
                //     "Security Baseline for Windows 10 and later",
                //     "Defender",
                //     "Block Office applications from creating executable content",
                //     "Not configured",
                //     "",
                //     "",
                //     "",
                //     ""
                // );
                // _test?.Pass("Successfully edited Profile - set to Not configured");

                // var editScreenshot4 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_NotConfigured_27995902");
                // if (!string.IsNullOrEmpty(editScreenshot4))
                // {
                //     _test?.Pass("Profile edit to Not configured completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot4).Build());
                // }

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_27995902");
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
