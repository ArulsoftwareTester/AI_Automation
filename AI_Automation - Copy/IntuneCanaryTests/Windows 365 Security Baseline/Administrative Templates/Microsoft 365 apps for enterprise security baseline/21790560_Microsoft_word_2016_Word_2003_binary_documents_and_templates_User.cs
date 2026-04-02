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
    public class T21790560_Microsoft_Word_2016_Word_2003_Binary_Documents_And_Templates_User : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Microsoft 365 Apps for Enterprise Security Baseline - Microsoft Word 2016 Word 2003 Binary Documents And Templates User Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21790560");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_21790560_Microsoft_Word_2016_Word_2003_Binary_Documents_And_Templates_User()
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
                                                        "Word 2003 binary documents and templates (User)", // Parent dropdown name
                                                        "Enabled",                        // Parent value - Config 1
                                                        "File block setting: (User)",     // Child dropdown name
                                                        "Open/Save blocked, use open policy", // Child value - Config 2
                                                        "Word Options > Security > Trust Center > File Block Settings",  // Parent path
                                                        ""                                // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile with Config 1: Parent=Enabled, Child=Open/Save blocked, use open policy");

                _test?.Info("Step 3: Editing Profile - Config 3");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Word 2016",
                                                        "Word 2003 binary documents and templates (User)", // Parent dropdown name
                                                        "Enabled",                        // Parent stays Enabled
                                                        "File block setting: (User)",     // Child dropdown name
                                                        "Do not block",                   // Child value - Config 3
                                                        "Word Options > Security > Trust Center > File Block Settings",  // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 3: Parent=Enabled, Child=Do not block");

                _test?.Info("Step 4: Editing Profile - Config 4");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Word 2016",
                                                        "Word 2003 binary documents and templates (User)", // Parent dropdown name
                                                        "Disabled",                       // Parent value - Config 4
                                                        "",                               // No child dropdown
                                                        "",                               // No child value
                                                        "Word Options > Security > Trust Center > File Block Settings",  // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 4: Parent=Disabled");

                _test?.Info("Step 5: Editing Profile - Config 5");
                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Word 2016",
                                                        "Word 2003 binary documents and templates (User)", // Parent dropdown name
                                                        "Not configured",                 // Parent value - Config 5
                                                        "",                               // No child dropdown
                                                        "",                               // No child value
                                                        "Word Options > Security > Trust Center > File Block Settings",  // Parent path
                                                        ""                                // Child path
                                    );
                _test?.Pass("Successfully edited Profile with Config 5: Parent=Not configured");

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }
                _test?.Pass("Test completed successfully!");

                // Test completed successfully
                await TestInitialize.LogSuccess(Page, "Test 21790560 completed successfully - All configurations tested", "Test_Complete_21790560");
                _test?.Pass("End-to-end test completed successfully with all configurations!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21790560");
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



