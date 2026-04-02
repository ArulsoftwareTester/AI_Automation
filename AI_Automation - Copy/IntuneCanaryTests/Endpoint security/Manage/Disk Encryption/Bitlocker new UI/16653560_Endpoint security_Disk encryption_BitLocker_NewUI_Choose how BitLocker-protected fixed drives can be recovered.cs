using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests.Manage {
    [TestFixture]
    public class T16653560_Endpoint_Security_Disk_Encryption_BitLocker_NewUI_Choose_How_BitLocker_Protected_Fixed_Drives_Can_Be_Recovered : PageTest
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
        public async Task Test_16653560_Endpoint_Security_Disk_Encryption_BitLocker_NewUI_Choose_How_BitLocker_Protected_Fixed_Drives_Can_Be_Recovered()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16653560 completed");
        }
    }
}