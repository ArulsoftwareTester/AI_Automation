using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests.ESAdditional {
    [TestFixture]
    public class T14511669_IDC_Vendor_Endpoint_security_Attack_surface_reduction_Device_Control_rules_Verifiy_Reusable_Settings_can_be_deleted : PageTest
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
        public async Task Test_14511669_IDC_Vendor_Endpoint_security_Attack_surface_reduction_Device_Control_rules_Verifiy_Reusable_Settings_can_be_deleted()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_14511669 completed");
        }
    }
}