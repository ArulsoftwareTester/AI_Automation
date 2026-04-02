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
    public class T27894686_Windows_Ink_Workspace_Allow_Windows_Ink_Workspace : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Windows Ink Workspace - Allow Windows Ink Workspace");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 27894686");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_27894686_Windows_Ink_Workspace_Allow_Windows_Ink_Workspace()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_27894686");
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
                    "Windows Ink Workspace",
                    "Allow Windows Ink Workspace",
                    "access to ink workspace is disabled. The feature is turned off.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully created NEW Profile with Config 1: access to ink workspace is disabled. The feature is turned off.");

                _test?.Info("Step 3: Editing Profile - Config 2");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Windows 365 Security Baseline",
                    "Windows Ink Workspace",
                    "Allow Windows Ink Workspace",
                    "ink workspace is enabled (feature is turned on), but the user cannot access it above the lock screen.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile with Config 2: ink workspace is enabled (feature is turned on), but the user cannot access it above the lock screen.");
                _test?.Info("Step 4: Editing Profile - Config 3");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Windows 365 Security Baseline",
                    "Windows Ink Workspace",
                    "Allow Windows Ink Workspace",
                    "ink workspace is enabled (feature is turned on), and the user is allowed to use it above the lock screen.",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile with Config 3: ink workspace is enabled (feature is turned on), and the user is allowed to use it above the lock screen");
                _test?.Info("Step 5: Editing Profile - Config 4");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Windows 365 Security Baseline",
                    "Windows Ink Workspace",
                    "Allow Windows Ink Workspace",
                    "Not configured",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile with Config 4: Not configured");

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_27894686");
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
