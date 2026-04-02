using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace IntuneCanaryTests
{
    public class SampleTest : PageTest
    {
        private ExtentTest? _test;
        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3 1.pfx"));
            Console.WriteLine($"Certificate path: {certPath}");
            return new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
                IgnoreHTTPSErrors = true,
                AcceptDownloads = false,
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Windows 365 Security Baseline - Allow cut/copy/paste from clipboard via script Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 28040953");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task TestEditCreatedProfile()
        {
            try
            {
                Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
                Console.WriteLine("  TEST EXECUTION STARTED: TestLogin");
                Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
                
                // Step 1: Login to Intune Portal using the new Login() function
                Console.WriteLine("[STEP 1] Logging into Intune Portal");
                Console.WriteLine("  → URL: https://aka.ms/intune");
                Console.WriteLine("  → Using certificate-based authentication\n");
                
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
                                                        "Allow drag and drop or copy and paste files",              // Parent dropdown name
                                                        "Disabled",                        // Parent dropdown value
                                                        "Allow drag and drop or copy and paste files", // Child dropdown name
                                                        "",                        // Child dropdown value
                                                        "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Restricted Sites Zone",  // Parent path
                                                        "Windows Components > Internet Explorer > Internet Control Panel > Security Page > Restricted Sites Zone"   // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile"); 

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }
                _test?.Pass("Test completed successfully!");
                
                Console.WriteLine("\n✅ RESULT: Successfully logged into Intune Portal");
                Console.WriteLine("   → Home page displayed");
                Console.WriteLine("   → User authenticated\n");
                
                Console.WriteLine("═══════════════════════════════════════════════════════════════");
                Console.WriteLine("  TEST EXECUTION COMPLETED: TestLogin PASSED");
                Console.WriteLine("═══════════════════════════════════════════════════════════════\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ TEST FAILED: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}\n");
                throw;
            }
        }
    }
}
