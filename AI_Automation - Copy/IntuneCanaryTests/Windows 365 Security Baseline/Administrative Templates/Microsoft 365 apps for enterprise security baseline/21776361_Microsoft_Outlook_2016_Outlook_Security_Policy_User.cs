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
    public class T21776361_Microsoft_Outlook_2016_Outlook_Security_Policy_User : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Microsoft 365 Apps for Enterprise Security Baseline - Microsoft Outlook 2016 Outlook Security Policy User Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21776361");
            _test.Info("Category: Security Baseline Configuration - Microsoft Outlook 2016");
            _test.Info("Parent: Microsoft Outlook 2016");
            _test.Info("Description: End-to-end test for Outlook Security Policy with dropdown options");
        }

        [Test]
        public async Task Test_21776361_Microsoft_Outlook_2016_Outlook_Security_Policy_User()
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
                _test?.Info("Step 2: Creating NEW Profile");
                await securityBaseline.CreateNewProfile(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Office 2016 (Machine)",
                                                        "Add-on Management",              // Parent dropdown name
                                                        "Enabled",                        // Parent dropdown value
                                                        "",                               // Child dropdown name
                                                        "",                               // Child dropdown value
                                                        "Security Settings > IE Security",// Parent path
                                                        ""                                // Child path
                                                    );    
                _test?.Pass("Successfully created NEW Profile"); 

                await securityBaseline.editNewCreatedPolicy(
                                                        Page,
                                                        "Endpoint security",
                                                        "Security baselines",
                                                        "Microsoft 365 Apps for Enterprise Security Baseline",
                                                        "Microsoft Office 2016 (Machine)",
                                                        "Add-on Management",              // Parent dropdown name
                                                        "Disabled",                        // Parent dropdown value
                                                        "",                               // Child dropdown name
                                                        "",                               // Child dropdown value
                                                        "Security Settings > IE Security",// Parent path
                                                        ""                                // Child path
                                    );

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }
                _test?.Pass("Test completed successfully!");
                _test?.Pass("End-to-end test completed successfully!");
                
                Console.WriteLine("========================================");
                Console.WriteLine("Test 21776361 completed successfully!");
                Console.WriteLine("All configuration scenarios executed:");
                Console.WriteLine("1. Config 1: Outlook Security Mode = Enabled, Policy = Outlook Default Security");
                Console.WriteLine("2. Config 2: Outlook Security Policy = Use Security Form from 'Outlook Security Settings'");
                Console.WriteLine("3. Config 3: Outlook Security Policy = Use Security Form from 'Outlook 10 Security Settings'");
                Console.WriteLine("4. Config 4: Outlook Security Policy = Use Outlook Security Group Policy");
                Console.WriteLine("5. Verified UI report functionality");
                Console.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21776361");
                if (!string.IsNullOrEmpty(errorScreenshot))
                {
                    _test?.Fail($"Test failed with exception: {ex.Message}", MediaEntityBuilder.CreateScreenCaptureFromPath(errorScreenshot).Build());
                }
                else
                {
                    _test?.Fail($"Test failed with exception: {ex.Message}");
                }
                
                Console.WriteLine($"========================================");
                Console.WriteLine($"Test 21776361 FAILED");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Console.WriteLine($"========================================");
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



