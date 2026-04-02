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
    public class T21783387_Microsoft_Outlook_2016_Configure_Outlook_Object_Model_Prompt_When_Sending_Mail_User : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Microsoft 365 Apps for Enterprise Security Baseline - Microsoft Outlook 2016 Configure Outlook Object Model Prompt When Sending Mail User Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21783387");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_21783387_Microsoft_Outlook_2016_Configure_Outlook_Object_Model_Prompt_When_Sending_Mail_User()
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
                                                        "Enabled",                        // Parent value - Config 1
                                                        "Configure Outlook object model prompt when sending mail (User)", // Child dropdown name
                                                        "Enabled",                        // Child value - Config 1
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile with Config 1: Parent=Enabled, Child=Enabled");

                _test?.Info("Step 3: Editing Profile - Config 2");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Outlook 2016",
                                                        "Outlook Security Mode (User)",   // Parent dropdown name
                                                        "Disabled",                       // Parent value - Config 2
                                                        "",                               // Child dropdown name (empty for Disabled parent)
                                                        "",                               // Child dropdown value
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 2: Parent=Disabled");

                _test?.Info("Step 4: Editing Profile - Config 3");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Outlook 2016",
                                                        "Outlook Security Mode (User)",   // Parent dropdown name
                                                        "Not configured",                 // Parent value - Config 3
                                                        "Configure Outlook object model prompt when sending mail (User)", // Child dropdown name
                                                        "Disabled",                       // Child value - Config 3
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 3: Parent=Not configured, Child=Disabled");

                _test?.Info("Step 5: Editing Profile - Config 4");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Outlook 2016",
                                                        "Outlook Security Mode (User)",   // Parent dropdown name
                                                        "Not configured",                 // Parent value - Config 4
                                                        "Configure Outlook object model prompt when sending mail (User)", // Child dropdown name
                                                        "Not configured",                 // Child value - Config 4
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 4: Parent=Not configured, Child=Not configured");

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }
                _test?.Pass("Test completed successfully!");

                // Test completed successfully
                await TestInitialize.LogSuccess(Page, "Test 21783387 completed successfully - All configurations tested", "Test_Complete_21783387");
                _test?.Pass("End-to-end test completed successfully with all configurations!");
                
                Console.WriteLine("========================================");
                Console.WriteLine("Test 21783387 completed successfully!");
                Console.WriteLine("All configuration scenarios executed:");
                Console.WriteLine("1. Config 1: Parent=Enabled, Child=Enabled");
                Console.WriteLine("2. Config 2: Parent=Disabled");
                Console.WriteLine("3. Config 3: Parent=Not configured, Child=Disabled");
                Console.WriteLine("4. Config 4: Parent=Not configured, Child=Not configured");
                Console.WriteLine("Configure Outlook object model prompt when sending mail");
                Console.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21783387");
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



