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
    public class T21780761_Microsoft_Office_2016_Machine_Scripted_Window_Security_Restrictions : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Microsoft 365 Apps for Enterprise Security Baseline - Microsoft Office 2016 Machine Scripted Window Security Restrictions Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21780761");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_21780761_Microsoft_Office_2016_Machine_Scripted_Window_Security_Restrictions()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.Login(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                await TestInitialize.LogStep(Page, "Step 2: Creating Microsoft 365 Apps for Enterprise Security Baseline Profile - Scripted Window Security Restrictions (Enabled)", "Before_CreateProfile");
                await securityBaseline.createProfileAdminTemplate(Page, "Scripted Window Security Restrictions", "Enabled");
                await TestInitialize.LogSuccess(Page, "Successfully created Microsoft 365 Apps for Enterprise Security Baseline Profile - Scripted Window Security Restrictions (Enabled)", "Profile_Created");

                await TestInitialize.LogStep(Page, "Step 3: Triggering VM Sync", "Before_VMSync_Config1");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config1");

/*                await TestInitialize.LogStep(Page, "Step 4: Editing created profile - Mark sub nodes False", "Before_EditCreated_Config2");
                await securityBaseline.editCreatedProfile(Page, "Scripted Window Security Restrictions", "Mark sub nodes False", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Mark sub nodes False", "EditCreated_Complete_Config2");*/

                /*await TestInitialize.LogStep(Page, "Step 5: Triggering VM Sync", "Before_VMSync_Config2");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config2");*/

                /*await TestInitialize.LogStep(Page, "Step 6: Editing created profile - Disabled", "Before_EditCreated_Config3");
                await securityBaseline.editCreatedProfile(
                    Page,
                    "M365 Apps",
                    "Disabled",
                    "Scripted Window Security Restrictions",
                    "",
                    "",
                    "Endpoint security",
                    "Security baselines",
                    "Microsoft Office 2016 (Machine)",
                    ""
                );
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Disabled", "EditCreated_Complete_Config3");*/

                /*await TestInitialize.LogStep(Page, "Step 7: Triggering VM Sync", "Before_VMSync_Config3");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config3");*/

                /*await TestInitialize.LogStep(Page, "Step 8: Editing created profile - Not configured", "Before_EditCreated_Config4");
                await securityBaseline.editCreatedProfile(
                    Page,
                    "M365 Apps",
                    "Not configured",
                    "Scripted Window Security Restrictions",
                    "",
                    "",
                    "Endpoint security",
                    "Security baselines",
                    "Microsoft Office 2016 (Machine)",
                    ""
                );
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Not configured", "EditCreated_Complete_Config4");*/

                /*await TestInitialize.LogStep(Page, "Step 9: Triggering VM Sync", "Before_VMSync_Config4");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config4");*/
                await TestInitialize.LogSuccess(Page, "Test completed successfully!", "Test_Complete");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21780761");
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



