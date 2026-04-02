using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class DCV_Policy : PageTest
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
        public async Task Test_CreateDCV_Policy()
        {
            try
            {
                Console.WriteLine("Test_CreateDCV_Policy started...");
                
                // Call IPLogin function from SecurityBaseline
                Console.WriteLine("Step 1: Logging into Intune Portal");
                var securityBaseline = new SecurityBaseline();
                await securityBaseline.IPLogin(Page);
                Console.WriteLine("Successfully logged into Intune Portal");
                
                // Call CreateDCV_Policy function from SecurityBaseline
                Console.WriteLine("Step 2: Executing CreateDCV_Policy");
                await securityBaseline.CreateDCV_Policy(Page);
                Console.WriteLine("Successfully completed CreateDCV_Policy");
                
                Console.WriteLine("Test completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed with error: {ex.Message}");
                throw;
            }
        }
    }
}
