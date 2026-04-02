using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8831316_Microsoft_Edge_Browser_Start_experience_Home_button : PageTest
    {
        public override BrowserNewContextOptions ContextOptions()
        {
            var certPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "auth-cert", "AIAutoPE_3.pfx"));
            Console.WriteLine($"Certificate path: {certPath}");
            return new BrowserNewContextOptions
            {
                IgnoreHTTPSErrors = true,
                ClientCertificates = new[]
                {
                    new ClientCertificate
                    {
                        Origin = "https://certauth.login.microsoftonline.com",
                        PfxPath = certPath,
                        Passphrase = "Admin@123"
                    }
                }
            };
        }

        [Test]
        public async Task Test_8831316_Microsoft_Edge_Browser_Start_experience_Home_button()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_8831316 completed");
        }
    }
}
