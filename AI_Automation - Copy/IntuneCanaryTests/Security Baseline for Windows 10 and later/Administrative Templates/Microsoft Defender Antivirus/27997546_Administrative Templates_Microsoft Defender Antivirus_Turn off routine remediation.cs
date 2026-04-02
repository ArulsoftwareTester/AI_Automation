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
    public class Test_27997546_MicrosoftDefenderAntivirus_TurnOffRoutineRemediation : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Security Baseline for Windows 10 and later - Microsoft Defender Antivirus - Turn off routine remediation");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 27997546");
            _test.Info("Category: Security Baseline Configuration - Microsoft Defender Antivirus");
        }

        [Test]
        public async Task Test_27997546_MicrosoftDefenderAntivirus_TurnOffRoutineRemediation()
        {
            try
            {
                Console.WriteLine("Test_27997546_MicrosoftDefenderAntivirus_TurnOffRoutineRemediation started...");
                _test?.Info("Test execution started");
                
                // Call IPLogin function from SecurityBaseline
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
                                                        "Security Baseline for Windows 10 and later",
                                                        "Microsoft Defender Antivirus",
                                                        "Turn off routine remediation",              // Parent dropdown name
                                                        "Enabled",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "Windows Components > Microsoft Defender Antivirus",  // Parent path
                                                        "Windows Components > Microsoft Defender Antivirus"   // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile"); 

                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Security Baseline for Windows 10 and later",
                                                        "Microsoft Defender Antivirus",
                                                        "Turn off routine remediation",              // Parent dropdown name
                                                        "Disabled",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "Windows Components > Microsoft Defender Antivirus",  // Parent path
                                                        "Windows Components > Microsoft Defender Antivirus"   // Child path
                                    );

                                    await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Security Baseline for Windows 10 and later",
                                                        "Microsoft Defender Antivirus",
                                                        "Turn off routine remediation",              // Parent dropdown name
                                                        "Not configured",                        // Parent dropdown value
                                                        "", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "Windows Components > Microsoft Defender Antivirus",  // Parent path
                                                        "Windows Components > Microsoft Defender Antivirus"   // Child path
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
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_27997546");
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
