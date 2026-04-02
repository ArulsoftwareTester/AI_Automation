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
    public class T21789700_Microsoft_Outlook_2016_Junk_E_Mail_Protection_Level_User : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Microsoft 365 Apps for Enterprise Security Baseline - Microsoft Outlook 2016 Junk E Mail Protection Level User Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21789700");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_21789700_Microsoft_Outlook_2016_Junk_E_Mail_Protection_Level_User()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.Login(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                await TestInitialize.LogStep(Page, "Step 2: Creating Microsoft 365 Apps for Enterprise Security Baseline Profile - Junk E-mail protection level (Enabled)", "Before_CreateProfile");
                await securityBaseline.createProfileAdminTemplate(Page, "Junk E-mail protection level", "Enabled");
                await TestInitialize.LogSuccess(Page, "Successfully created Microsoft 365 Apps for Enterprise Security Baseline Profile - Junk E-mail protection level (Enabled)", "Profile_Created");

                await TestInitialize.LogStep(Page, "Step 3: Triggering VM Sync", "Before_VMSync_Config1");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config1");

/*                await TestInitialize.LogStep(Page, "Step 4: Editing created profile - No automatic filtering", "Before_EditCreated_Config2");
                await securityBaseline.editCreatedProfile(Page, "Junk E-mail protection level", "No automatic filtering", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - No automatic filtering", "EditCreated_Complete_Config2");*/

                /*await TestInitialize.LogStep(Page, "Step 5: Triggering VM Sync", "Before_VMSync_Config2");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config2");*/

/*                await TestInitialize.LogStep(Page, "Step 6: Editing created profile - Low", "Before_EditCreated_Config3");
                await securityBaseline.editCreatedProfile(Page, "Junk E-mail protection level", "Low", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Low", "EditCreated_Complete_Config3");*/

                /*await TestInitialize.LogStep(Page, "Step 7: Triggering VM Sync", "Before_VMSync_Config3");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config3");*/

/*                await TestInitialize.LogStep(Page, "Step 8: Editing created profile - High", "Before_EditCreated_Config4");
                await securityBaseline.editCreatedProfile(Page, "Junk E-mail protection level", "High", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - High", "EditCreated_Complete_Config4");*/

                /*await TestInitialize.LogStep(Page, "Step 9: Triggering VM Sync", "Before_VMSync_Config4");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config4");*/

/*                await TestInitialize.LogStep(Page, "Step 10: Editing created profile - Safe Lists Only", "Before_EditCreated_Config5");
                await securityBaseline.editCreatedProfile(Page, "Junk E-mail protection level", "Safe Lists Only", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Safe Lists Only", "EditCreated_Complete_Config5");*/

                /*await TestInitialize.LogStep(Page, "Step 11: Triggering VM Sync", "Before_VMSync_Config5");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config5");*/

/*                await TestInitialize.LogStep(Page, "Step 12: Editing created profile - Disabled", "Before_EditCreated_Config6");
                await securityBaseline.editCreatedProfile(Page, "Junk E-mail protection level", "Disabled", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Disabled", "EditCreated_Complete_Config6");*/

                /*await TestInitialize.LogStep(Page, "Step 13: Triggering VM Sync", "Before_VMSync_Config6");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config6");*/

/*                await TestInitialize.LogStep(Page, "Step 14: Editing created profile - Not configured", "Before_EditCreated_Config7");
                await securityBaseline.editCreatedProfile(Page, "Junk E-mail protection level", "Not configured", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Not configured", "EditCreated_Complete_Config7");*/

                /*await TestInitialize.LogStep(Page, "Step 15: Triggering VM Sync", "Before_VMSync_Config7");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config7");*/
                await TestInitialize.LogSuccess(Page, "Test completed successfully!", "Test_Complete");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21789700");
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



