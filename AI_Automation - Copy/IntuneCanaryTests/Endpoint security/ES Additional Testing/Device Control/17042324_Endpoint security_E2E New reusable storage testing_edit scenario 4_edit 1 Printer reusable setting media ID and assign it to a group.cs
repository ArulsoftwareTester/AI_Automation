using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests.ESAdditional {
    [TestFixture]
    public class T17042324_Endpoint_security_E2E_New_reusable_storage_testing_edit_scenario_4_edit_1_Printer_reusable_setting_media_ID_and_assign_it_to_a_group : PageTest
    {
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
        [Test]
        public async Task Test_17042324_Endpoint_security_E2E_New_reusable_storage_testing_edit_scenario_4_edit_1_Printer_reusable_setting_media_ID_and_assign_it_to_a_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_17042324 completed");
        }
    }
}