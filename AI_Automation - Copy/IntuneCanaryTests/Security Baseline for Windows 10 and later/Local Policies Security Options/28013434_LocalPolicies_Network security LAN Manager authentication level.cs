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
    public class LocalPolicies_28013434_NetworkSecurityLANManagerAuthenticationLevel : PageTest
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
            _test = TestInitialize.CreateTest(TestContext.CurrentContext.Test.Name, "Security Baseline for Windows 10 and later - Local Policies Security Options: Network security LAN Manager authentication level");
            _test.Info($"Test started at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            _test.Info("Test ID: 28013434");
            _test.Info("Category: Security Baseline for Windows 10 and later - Local Policies Security Options");
        }

        [Test]
        public async Task Test_28013434_LocalPolicies_NetworkSecurityLANManagerAuthenticationLevel()
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
                
                _test?.Info("Step 2: Creating NEW Profile for Local Policies Security Options - Network Security LAN Manager Authentication Level");
                await securityBaseline.CreateNewProfile(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "Network Security LAN Manager Authentication Level",
                    "Send LM and NTLMv2 responses only. Refuse LM and NTLM",
                    "",
                    "",
                    "",
                    ""
                );    
                _test?.Pass("Successfully created NEW Profile for Network Security LAN Manager Authentication Level"); 

                var profileScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Created_28013434");
                if (!string.IsNullOrEmpty(profileScreenshot))
                {
                    _test?.Pass("Profile creation completed", MediaEntityBuilder.CreateScreenCaptureFromPath(profileScreenshot).Build());
                }

                _test?.Info("Step 3: Editing Profile - Changing Network Security LAN Manager Authentication Level to Send LM & NTLM responses");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "Network Security LAN Manager Authentication Level",
                    "Send LM & NTLM responses",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - Network Security LAN Manager Authentication Level set to Send LM & NTLM responses");

                var editScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28013434");
                if (!string.IsNullOrEmpty(editScreenshot))
                {
                    _test?.Pass("Profile edit completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot).Build());
                }

                _test?.Info("Step 4: Editing Profile - Changing Network Security LAN Manager Authentication Level to Send LM and NTLM-use NTLMv2 session security if negotiated");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "Network Security LAN Manager Authentication Level",
                    "Send LM and NTLM-use NTLMv2 session security if negotiated",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - Network Security LAN Manager Authentication Level set to Send LM and NTLM-use NTLMv2 session security if negotiated");

                var editScreenshot2 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28013434_2");
                if (!string.IsNullOrEmpty(editScreenshot2))
                {
                    _test?.Pass("Profile edit 2 completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot2).Build());
                }

                _test?.Info("Step 5: Editing Profile - Changing Network Security LAN Manager Authentication Level to Send LM and NTLM responses only");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "Network Security LAN Manager Authentication Level",
                    "Send LM and NTLM responses only",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - Network Security LAN Manager Authentication Level set to Send LM and NTLM responses only");

                var editScreenshot3 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28013434_3");
                if (!string.IsNullOrEmpty(editScreenshot3))
                {
                    _test?.Pass("Profile edit 3 completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot3).Build());
                }

                _test?.Info("Step 6: Editing Profile - Changing Network Security LAN Manager Authentication Level to Send LM and NTLMv2 responses only");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "Network Security LAN Manager Authentication Level",
                    "Send LM and NTLMv2 responses only",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - Network Security LAN Manager Authentication Level set to Send LM and NTLMv2 responses only");

                var editScreenshot4 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28013434_4");
                if (!string.IsNullOrEmpty(editScreenshot4))
                {
                    _test?.Pass("Profile edit 4 completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot4).Build());
                }

                _test?.Info("Step 7: Editing Profile - Changing Network Security LAN Manager Authentication Level to Send LM and NTLMv2 responses only. Refuse LM");
                await securityBaseline.editNewCreatedPolicy(
                    Page,
                    "Endpoint security",
                    "Security baselines",
                    "Security Baseline for Windows 10 and later",
                    "Local Policies Security Options",
                    "Network Security LAN Manager Authentication Level",
                    "Send LM and NTLMv2 responses only. Refuse LM",
                    "",
                    "",
                    "",
                    ""
                );
                _test?.Pass("Successfully edited Profile - Network Security LAN Manager Authentication Level set to Send LM and NTLMv2 responses only. Refuse LM");

                var editScreenshot5 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28013434_5");
                if (!string.IsNullOrEmpty(editScreenshot5))
                {
                    _test?.Pass("Profile edit 5 completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot5).Build());
                }

                // _test?.Info("Step 8: Editing Profile - Changing Network Security LAN Manager Authentication Level to Not configured");
                // await securityBaseline.editNewCreatedPolicy(
                //     Page,
                //     "Endpoint security",
                //     "Security baselines",
                //     "Security Baseline for Windows 10 and later",
                //     "Local Policies Security Options",
                //     "Network security LAN Manager authentication level",
                //     "Not configured",
                //     "",
                //     "",
                //     "",
                //     ""
                // );
                // _test?.Pass("Successfully edited Profile - Network security LAN Manager authentication level set to Not configured");

                // var editScreenshot6 = await ExtentReportHelper.CaptureScreenshot(Page, "Profile_Edited_28013434_6");
                // if (!string.IsNullOrEmpty(editScreenshot6))
                // {
                //     _test?.Pass("Profile edit 6 completed", MediaEntityBuilder.CreateScreenCaptureFromPath(editScreenshot6).Build());
                // }

                _test?.Pass("Test completed successfully!");
            }
            catch (Exception ex)
            {
                var errorScreenshot = await ExtentReportHelper.CaptureScreenshot(Page, "Error_28013434");
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
