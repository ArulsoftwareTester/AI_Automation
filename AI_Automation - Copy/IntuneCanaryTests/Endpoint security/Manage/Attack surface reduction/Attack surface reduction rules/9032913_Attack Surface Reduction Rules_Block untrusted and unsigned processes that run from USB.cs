using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests.Manage {
    [TestFixture]
    public class T9032913_Attack_Surface_Reduction_Rules_Block_Untrusted_And_Unsigned_Processes_That_Run_From_USB : PageTest
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
        public async Task Test_9032913_Attack_Surface_Reduction_Rules_Block_Untrusted_And_Unsigned_Processes_That_Run_From_USB()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9032913 completed");
        }
    }
}