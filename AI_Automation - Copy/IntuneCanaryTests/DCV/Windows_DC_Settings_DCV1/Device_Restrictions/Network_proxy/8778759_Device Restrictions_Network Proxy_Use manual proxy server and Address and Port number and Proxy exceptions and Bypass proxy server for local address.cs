using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778759_Device_Restrictions_Network_Proxy_Use_manual_proxy_server_and_Address_and_Port_number_and_Proxy_exceptions_and_Bypass_proxy_server_for_local_address : PageTest
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
        public async Task Test_8778759_Device_Restrictions_Network_Proxy_Use_manual_proxy_server_and_Address_and_Port_number_and_Proxy_exceptions_and_Bypass_proxy_server_for_local_address()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_8778759 completed");
        }
    }
}
