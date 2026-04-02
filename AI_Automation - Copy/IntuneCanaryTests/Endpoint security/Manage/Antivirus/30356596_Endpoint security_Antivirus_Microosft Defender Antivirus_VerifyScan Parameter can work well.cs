using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests.Manage {
    [TestFixture]
    public class T30356596_Endpoint_Security_Antivirus_Verify_Scan_Parameter_Can_Work_Well : PageTest
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
        public async Task Test_30356596_Endpoint_Security_Antivirus_Verify_Scan_Parameter_Can_Work_Well()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            await securityBaseline.createProfile_Win365(Page, "Windows 365");
            await securityBaseline.MDMPolicySync(Page);
            Console.WriteLine("Test_30356596 completed");
        }
    }
}