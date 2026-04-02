using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using DotNetEnv;

namespace IntuneCanaryTests.G_and_T.AssignmentFilter
{
    public class Test_15568291_Filter_Basic_RA_Resource_access_policy : PageTest
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
        public async Task FilterBasic_RAResourceAccessPolicy()
        {
            Console.WriteLine("Starting test: 15568291_Filter Basic_RA_Resource access_policy");
            
            // Call IPLogin function
            await _securityBaseline.IPLogin(Page);
            
            // Add your test implementation here
            
            Console.WriteLine("Test completed successfully!");
        }
    }
}
