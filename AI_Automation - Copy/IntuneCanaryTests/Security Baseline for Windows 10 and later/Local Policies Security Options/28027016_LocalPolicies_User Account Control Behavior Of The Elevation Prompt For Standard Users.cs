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
    public class LocalPolicies_28027016_UACBehaviorElevationPromptStandardUsers : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Security Baseline for Windows 10 and later - Local Policies Security Options: User Account Control Behavior Of The Elevation Prompt For Standard Users");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 28027016");
            _test.Info("Category: Security Baseline for Windows 10 and later - Local Policies Security Options");
        }

        [Test]
        public async Task Test_28027016_LocalPolicies_UACBehaviorElevationPromptStandardUsers()
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
                
                _test?.Info("Step 2: Creating NEW Profile for Local Policies Security Options - User Account Control Behavior Of The Elevation Prompt For Standard Users");
                await securityBaseline.CreateNewProfile(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "User Account Control Behavior Of The Elevation Prompt For Standard Users",
                    "Automatically deny elevation requests",
                    "",
                    "",
                    "",
                    ""
                );    
                _test?.Pass("Successfully created NEW Profile for User Account Control Behavior Of The Elevation Prompt For Standard Users"); 

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created_28027016");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }

                _test?.Info("Step 3: Editing Profile - Changing User Account Control Behavior Of The Elevation Prompt For Standard Users to Prompt for credentials on the secure desktop");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "User Account Control Behavior Of The Elevation Prompt For Standard Users",
                    "Prompt for credentials on the secure desktop",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - User Account Control Behavior Of The Elevation Prompt For Standard Users set to Prompt for credentials on the secure desktop");

                var editScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28027016");
                if (!string.IsNullOrEmpty(editScreenshot))
                {
                    _test?.Pass("Profile edit completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot).Build());
                }

                _test?.Info("Step 4: Editing Profile - Changing User Account Control Behavior Of The Elevation Prompt For Standard Users to Prompt for credentials");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "User Account Control Behavior Of The Elevation Prompt For Standard Users",
                    "Prompt for credentials",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - User Account Control Behavior Of The Elevation Prompt For Standard Users set to Prompt for credentials");

                var editScreenshot2 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28027016_2");
                if (!string.IsNullOrEmpty(editScreenshot2))
                {
                    _test?.Pass("Profile edit completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot2).Build());
                }

                // _test?.Info("Step 5: Editing Profile - Changing User Account Control Behavior Of The Elevation Prompt For Standard Users to Not configured");
                // await securityBaseline.editNewCreatedPolicy(
                //     Page,
                //     "Endpoint security",
                //     "Security baselines",
                //     "Security Baseline for Windows 10 and later",
                //     "Local Policies Security Options",
                //     "User Account Control Behavior Of The Elevation Prompt For Standard Users",
                //     "Not configured",
                //     "",
                //     "",
                //     "",
                //     ""
                // );
                // _test?.Pass("Successfully edited Profile - User Account Control Behavior Of The Elevation Prompt For Standard Users set to Not configured");

                // var editScreenshot3 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28027016_3");
                // if (!string.IsNullOrEmpty(editScreenshot3))
                // {
                //     _test?.Pass("Profile edit completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot3).Build());
                // }

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_28027016");
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
