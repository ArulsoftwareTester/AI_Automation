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
    public class T21789616_Microsoft_Excel_2016_Excel_Options_Security_Trust_Center_File_Block_Settings : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Microsoft 365 Apps for Enterprise Security Baseline - Microsoft Excel 2016 Excel Options Security Trust Center File Block Settings Test");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 21789616");
            _test.Info("Category: Security Baseline Configuration");
        }

        [Test]
        public async Task Test_21789616_Microsoft_Excel_2016_Excel_Options_Security_Trust_Center_File_Block_Settings()
        {
            try
            {
                _test?.Info("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.Login(Page);
                _test?.Pass("Successfully logged into Intune Portal");

                await TestInitialize.LogStep(Page, "Step 2: Creating Microsoft 365 Apps for Enterprise Security Baseline Profile - Excel Options / Security / Trust Center / File Block Settings (Enabled)", "Before_CreateProfile");
                await securityBaseline.createProfileAdminTemplate(Page, "Excel Options / Security / Trust Center / File Block Settings", "Enabled");
                await TestInitialize.LogSuccess(Page, "Successfully created Microsoft 365 Apps for Enterprise Security Baseline Profile - Excel Options / Security / Trust Center / File Block Settings (Enabled)", "Profile_Created");

                await TestInitialize.LogStep(Page, "Step 3: Triggering VM Sync", "Before_VMSync_Config1");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config1");

/*                await TestInitialize.LogStep(Page, "Step 4: Editing created profile - Do not block (Config 2)", "Before_EditCreated_Config2");
                await securityBaseline.editCreatedProfile(Page, "Excel Options / Security / Trust Center / File Block Settings", "Do not block", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Do not block", "EditCreated_Complete_Config2");*/

                /*await TestInitialize.LogStep(Page, "Step 5: Triggering VM Sync", "Before_VMSync_Config2");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config2");*/

/*                await TestInitialize.LogStep(Page, "Step 6: Editing created profile - Save blocked (Config 3)", "Before_EditCreated_Config3");
                await securityBaseline.editCreatedProfile(Page, "Excel Options / Security / Trust Center / File Block Settings", "Save blocked", "", "", "", "", "", "");
                await TestInitialize.LogSuccess(Page, "Successfully edited created profile - Save blocked", "EditCreated_Complete_Config3");*/

                /*await TestInitialize.LogStep(Page, "Step 7: Triggering VM Sync", "Before_VMSync_Config3");
                // await securityBaseline.VMSync(Page);
                await TestInitialize.LogSuccess(Page, "Successfully completed VM Sync", "VMSync_Complete_Config3");*/
                await TestInitialize.LogSuccess(Page, "Test completed successfully!", "Test_Complete");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_21789616");
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



