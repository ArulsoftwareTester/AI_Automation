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
    public class T21778034_Microsoft_Outlook_2016_Authentication_With_Exchange_Server_User : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Microsoft 365 Apps for Enterprise Security Baseline - Microsoft Outlook 2016 Authentication With Exchange Server User Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21778034");
            _test.Info("Category: Security Baseline Configuration - Microsoft Outlook 2016");
            _test.Info("Parent: Microsoft Outlook 2016");
        }

        [Test]
        public async Task Test_21778034_Microsoft_Outlook_2016_Authentication_With_Exchange_Server_User()
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
                _test?.Info("Step 2: Creating NEW Profile - Config 1");
                await securityBaseline.CreateNewProfile(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Outlook 2016",
                                                        "Outlook Security Mode (User)",   // Parent dropdown name
                                                        "Enabled",                        // Parent dropdown value
                                                        "Authentication with Exchange Server (User)",  // Child dropdown name
                                                        "Enabled",                        // Child dropdown value
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile with Config 1: Outlook Security Mode=Enabled, Authentication with Exchange Server=Enabled"); 

                _test?.Info("Step 3: Editing Profile - Config 2");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Outlook 2016",
                                                        "Outlook Security Mode (User)",   // Parent dropdown name
                                                        "Disabled",                       // Parent dropdown value
                                                        "",                               // Child dropdown name
                                                        "",                               // Child dropdown value
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 2: Outlook Security Mode=Disabled");

                _test?.Info("Step 4: Editing Profile - Config 3");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Outlook 2016",
                                                        "Outlook Security Mode (User)",   // Parent dropdown name
                                                        "Not configured",                 // Parent dropdown value
                                                        "Authentication with Exchange Server (User)",  // Child dropdown name
                                                        "Disabled",                       // Child dropdown value
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 3: Outlook Security Mode=Not configured, Authentication with Exchange Server=Disabled");

                _test?.Info("Step 5: Editing Profile - Config 4");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Outlook 2016",
                                                        "",                               // Parent dropdown name
                                                        "",                               // Parent dropdown value
                                                        "Authentication with Exchange Server (User)",  // Child dropdown name
                                                        "Not configured",                 // Child dropdown value
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 4: Authentication with Exchange Server=Not configured");

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }
                _test?.Pass("Test completed successfully!");

                // Test completed successfully
                await TestInitialize.LogSuccess(Page, "Test 21778034 completed successfully - All configurations tested", "Test_Complete_21778034");
                _test?.Pass("End-to-end test completed successfully with all configurations!");
                
                Console.WriteLine("========================================");
                Console.WriteLine("Test 21778034 completed successfully!");
                Console.WriteLine("All configuration scenarios executed:");
                Console.WriteLine("1. Logged into Intune Portal");
                Console.WriteLine("2. Config 1: Created Profile with Outlook Security Mode=Enabled, Authentication with Exchange Server=Enabled");
                Console.WriteLine("3. Config 2: Modified Outlook Security Mode=Disabled");
                Console.WriteLine("4. Config 3: Modified Outlook Security Mode=Not configured, Authentication with Exchange Server=Disabled");
                Console.WriteLine("5. Config 4: Modified Authentication with Exchange Server=Not configured");
                Console.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21778034");
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



