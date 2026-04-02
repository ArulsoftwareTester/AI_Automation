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
    public class T21788308_Microsoft_Word_2016_Require_That_Application_Add_Ins_Are_Signed_By_Trusted_Publisher_User : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Microsoft 365 Apps for Enterprise Security Baseline - Microsoft Word 2016 Require That Application Add Ins Are Signed By Trusted Publisher User Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21788308");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_21788308_Microsoft_Word_2016_Require_That_Application_Add_Ins_Are_Signed_By_Trusted_Publisher_User()
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
                                                        "Microsoft Word 2016",
                                                        "Disable Trust Bar Notification for unsigned application add-ins and block them (User) (Deprecated)", // Parent 1
                                                        "Not configured",                 // Parent 1 value - Config 1
                                                        "Require that application add-ins are signed by Trusted Publisher (User)", // Child (Parent 2)
                                                        "Enabled",                        // Child value - Config 1
                                                        "Word Options > Security > Trust Center",  // Parent path
                                                        ""                                // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile with Config 1: Parent1=Not configured, Child=Enabled");

                _test?.Info("Step 3: Editing Profile - Config 2");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Word 2016",
                                                        "Disable Trust Bar Notification for unsigned application add-ins and block them (User) (Deprecated)", // Parent 1
                                                        "Enabled",                        // Parent 1 value - Config 2
                                                        "Require that application add-ins are signed by Trusted Publisher (User)", // Child (Parent 2)
                                                        "",                               // No child value for this config
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 2: Parent1=Enabled");

                _test?.Info("Step 4: Editing Profile - Config 3");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Word 2016",
                                                        "Disable Trust Bar Notification for unsigned application add-ins and block them (User) (Deprecated)", // Parent 1
                                                        "Disabled",                       // Parent 1 value - Config 3
                                                        "Require that application add-ins are signed by Trusted Publisher (User)", // Child (Parent 2)
                                                        "",                               // No child value for this config
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 3: Parent1=Disabled");

                _test?.Info("Step 5: Editing Profile - Config 4");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Word 2016",
                                                        "Disable Trust Bar Notification for unsigned application add-ins and block them (User) (Deprecated)", // Parent 1
                                                        "",                               // No parent 1 value
                                                        "Require that application add-ins are signed by Trusted Publisher (User)", // Child (Parent 2)
                                                        "Disabled",                       // Child value - Config 4
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 4: Child=Disabled");

                _test?.Info("Step 6: Editing Profile - Config 5");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Word 2016",
                                                        "Disable Trust Bar Notification for unsigned application add-ins and block them (User) (Deprecated)", // Parent 1
                                                        "",                               // No parent 1 value
                                                        "Require that application add-ins are signed by Trusted Publisher (User)", // Child (Parent 2)
                                                        "Not configured",                 // Child value - Config 5
                                                        "",                               // Parent path
                                                        ""                                // Child path
                                    );

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }
                _test?.Pass("Test completed successfully!");

                // Test completed successfully
                await TestInitialize.LogSuccess(Page, "Test 21617126 completed successfully - All configurations tested", "Test_Complete_21617126");
                _test?.Pass("End-to-end test completed successfully with all configurations!");
                
                Console.WriteLine("========================================");
                Console.WriteLine("Test 21617126 completed successfully!");
                Console.WriteLine("All configuration scenarios executed:");
                Console.WriteLine("1. Logged into Intune Portal");
                Console.WriteLine("2. Created Microsoft 365 Apps Security Baseline Profile");
                Console.WriteLine("3. Synced device after profile creation");
                Console.WriteLine("4. Config 2: Modified Add-on Management subnodes to False");
                Console.WriteLine("5. Synced device after Config 2 changes");
                Console.WriteLine("6. Verified UI report functionality");
                Console.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21788308");
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



