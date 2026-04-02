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
    public class Firewall_27981086_Win10_EnablePublicNetworkFirewall : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Security Baseline for Windows 10 and later - Firewall: Enable Public Network Firewall");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 27981086");
            _test.Info("Category: Security Baseline for Windows 10 and later - Firewall");
        }

        [Test]
        public async Task Test_27981086_Firewall_Win10_EnablePublicNetworkFirewall()
        {
            try
            {
                _test?.Info("Test execution started");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_27981086");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                await securityBaseline.createProfileAdminTemplate(Page, "Security Baseline for Windows 10 and later", "Yes. Turns on Windows Defender Firewall.", "Firewall", "Enable Public Network Firewall");
                _test?.Pass("Successfully created Security Baseline for Windows 10 and later Profile");

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created_27981086");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }

                await securityBaseline.VMSync(Page);
                _test?.Pass("Successfully completed VM Sync after profile creation");

                var vmSyncScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "VMSync_After_Profile_Creation_27981086");
                if (!string.IsNullOrEmpty(vmSyncScreenshot))
                {
                    _test?.Pass("VM Sync completed", MediaEntityBuilder.CreateScreenCaptureFromPath(vmSyncScreenshot).Build());
                }

                await securityBaseline.editCreatedProfile(Page, "Windows 10", "No. Turns off Windows Defender Firewall.", "Enable Public Network Firewall", "Endpoint security", "Security baselines", "Firewall", "", "", "");
                _test?.Pass("Successfully edited Security Baseline Profile - changed Enable Public Network Firewall to No");

                var editScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_No_27981086");
                if (!string.IsNullOrEmpty(editScreenshot))
                {
                    _test?.Pass("Profile edit to No completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot).Build());
                }

                await securityBaseline.VMSync(Page);
                _test?.Pass("Successfully completed VM Sync after first edit");

                var vmSyncScreenshot1 = await ExtentReportHelper.CaptureScreenshot(Page, "VMSync_After_First_Edit_27981086");
                if (!string.IsNullOrEmpty(vmSyncScreenshot1))
                {
                    _test?.Pass("VM Sync completed", MediaEntityBuilder.CreateScreenCaptureFromPath(vmSyncScreenshot1).Build());
                }

                await securityBaseline.editCreatedProfile(Page, "Windows 10", "Not configured", "Enable Public Network Firewall", "Endpoint security", "Security baselines", "Firewall", "", "", "");
                _test?.Pass("Successfully edited Security Baseline Profile - changed Enable Public Network Firewall to Not configured");

                var editScreenshot2 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_NotConfigured_27981086");
                if (!string.IsNullOrEmpty(editScreenshot2))
                {
                    _test?.Pass("Profile edit to Not configured completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot2).Build());
                }

                await securityBaseline.VMSync(Page);
                _test?.Pass("Successfully completed VM Sync after second edit");

                var vmSyncScreenshot2 = await ExtentReportHelper.CaptureScreenshot(Page, "VMSync_After_Second_Edit_27981086");
                if (!string.IsNullOrEmpty(vmSyncScreenshot2))
                {
                    _test?.Pass("VM Sync completed", MediaEntityBuilder.CreateScreenCaptureFromPath(vmSyncScreenshot2).Build());
                }

                // await securityBaseline.MDMPolicySync(Page);
                // _test?.Pass("Successfully completed MDM Policy Sync");
                // _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_27981086");
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
