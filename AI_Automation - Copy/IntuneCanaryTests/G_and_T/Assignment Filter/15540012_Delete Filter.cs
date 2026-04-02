using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using DotNetEnv;

namespace IntuneCanaryTests.G_and_T.AssignmentFilter
{
    public class Test_15540012_Delete_Filter : PageTest
    {
        private SecurityBaseline _securityBaseline;

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
        public void Setup()
        {
            _securityBaseline = new SecurityBaseline();
        }

        [Test]
        public async Task DeleteFilter()
        {
            Console.WriteLine("Starting test: 15540012_Delete Filter");
            
            // Call IPLogin function
            await _securityBaseline.IPLogin(Page);
            
            // Add your test implementation here
            
            Console.WriteLine("Test completed successfully!");
        }
    }
}
