using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using DotNetEnv;

namespace IntuneCanaryTests.G_and_T.AssignmentFilter
{
    public class Test_16355805_Filter_Associated_assignments_ADMX_DCv1_Check_the_properties_from_the_link_of_ADMX_DCv1_name : PageTest
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
        public async Task FilterAssociatedAssignments_ADMXDCv1_CheckThePropertiesFromTheLinkOfADMXDCv1Name()
        {
            Console.WriteLine("Starting test: 16355805_Filter Associated assignments_ADMX DCv1_Check the properties from the link of ADMX DCv1 name");
            
            // Call IPLogin function
            await _securityBaseline.IPLogin(Page);
            
            // Add your test implementation here
            
            Console.WriteLine("Test completed successfully!");
        }
    }
}
