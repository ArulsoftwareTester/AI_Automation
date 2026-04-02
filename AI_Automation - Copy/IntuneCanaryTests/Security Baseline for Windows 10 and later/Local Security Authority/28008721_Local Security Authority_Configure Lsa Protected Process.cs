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
    public class T28008721_ConfigureLsaProtectedProcess : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Local Security Authority - Configure Lsa Protected Process");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 28008721");
            _test.Info("Category: Local Security Authority");
        }

        [Test]
        public async Task Test_28008721_ConfigureLsaProtectedProcess()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                var loginScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Login_Complete_28008721");
                if (!string.IsNullOrEmpty(loginScreenshot))
                {
                    _test?.Info("Login completed", MediaEntityBuilder.CreateScreenCaptureFromPath(loginScreenshot).Build());
                }

                await securityBaseline.CreateNewProfile(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Windows 365 Security Baseline",
                                                        "Local Security Authority",
                                                        "Configure Lsa Protected Process",   // Parent dropdown name
                                                        "Enabled with UEFI lock. LSA will run as protected process and this configuration is UEFI locked",   // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "",  // Parent path
                                                        ""   // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile"); 
                          await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Windows 365 Security Baseline",
                                                        "Local Security Authority",
                                                        "Configure Lsa Protected Process",              // Parent dropdown name
                                                        "Enabled without UEFI lock. LSA will run as protected process and this configuration is not UEFI locked.",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "",  // Parent path
                                                        ""   // Child path
                                    );

                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Windows 365 Security Baseline",
                                                        "Local Security Authority",
                                                        "Configure Lsa Protected Process",              // Parent dropdown name
                                                        "Disabled. Default value. LSA will not run as protected process.",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "",  // Parent path
                                                        ""   // Child path
                                    );
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Windows 365 Security Baseline",
                                                        "Local Security Authority",
                                                        "Configure Lsa Protected Process",              // Parent dropdown name
                                                        "Not configured",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "",  // Parent path
                                                        ""   // Child path
                                    );

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_28008721");
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
