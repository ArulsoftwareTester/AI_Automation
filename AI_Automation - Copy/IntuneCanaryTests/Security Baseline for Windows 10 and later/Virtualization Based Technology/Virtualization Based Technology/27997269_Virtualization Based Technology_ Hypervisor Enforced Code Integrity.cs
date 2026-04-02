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
    public class VirtualizationBasedTechnology_27997269_HypervisorEnforcedCodeIntegrity : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Security Baseline for Windows 10 and later - Virtualization Based Technology: Hypervisor Enforced Code Integrity");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 27997269");
            _test.Info("Category: Security Baseline for Windows 10 and later - Virtualization Based Technology");
        }

        [Test]
        public async Task Test_27997269_VirtualizationBasedTechnology_HypervisorEnforcedCodeIntegrity()
        {
            try
            {
                _test?.Info("Step 1: Login to Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.Login(Page);
                _test?.Pass("Successfully logged into Intune Portal");
                
                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                _test?.Info("Step 2: Create new Security Baseline profile with Hypervisor Enforced Code Integrity setting");
                await securityBaseline.CreateNewProfile(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Virtualization Based Technology",
                    "Hypervisor Enforced Code Integrity",
                    "(Disabled) Turns off Hypervisor-Protected Code Integrity remotely if configured previously without UEFI Lock.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully created Security Baseline for Windows 10 and later Profile");

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                } 

                _test?.Info("Step 3: Edit profile to change setting to Disabled");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Virtualization Based Technology",
                    "Hypervisor Enforced Code Integrity",
                    "(Enabled with UEFI lock) Turns on Hypervisor-Protected Code Integrity with UEFI lock.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Security Baseline Profile - changed setting to (Enabled with UEFI lock) Turns on Hypervisor-Protected Code Integrity with UEFI lock.");

                var editScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_EnabledWithUefiLock");
                if (!string.IsNullOrEmpty(editScreenshot))
                {
                    _test?.Pass("Profile edit to (Enabled with UEFI lock) Turns on Hypervisor-Protected Code Integrity with UEFI lock. completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot).Build());
                }

                _test?.Info("Step 4: Edit profile again to change setting to Enabled without lock");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Virtualization Based Technology",
                    "Hypervisor Enforced Code Integrity",
                    "(Enabled without lock) Turns on Hypervisor-Protected Code Integrity without UEFI lock.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Security Baseline Profile - changed setting to (Enabled without lock) Turns on Hypervisor-Protected Code Integrity without UEFI lock.");

                var editScreenshot2 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_EnabledWithoutLock");
                if (!string.IsNullOrEmpty(editScreenshot2))
                {
                    _test?.Pass("Profile edit to (Enabled without lock) Turns on Hypervisor-Protected Code Integrity without UEFI lock. completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot2).Build());
                }

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_27997269");
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
