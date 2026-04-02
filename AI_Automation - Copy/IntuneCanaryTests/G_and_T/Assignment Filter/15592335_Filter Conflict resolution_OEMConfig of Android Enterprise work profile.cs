using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using DotNetEnv;

namespace IntuneCanaryTests.G_and_T.AssignmentFilter
{
    public class Test_15592335_Filter_Conflict_resolution_OEMConfig_of_Android_Enterprise_work_profile : PageTest
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
        public async Task FilterConflictResolution_OEMConfigOfAndroidEnterpriseWorkProfile()
        {
            Console.WriteLine("Starting test: 15592335_Filter Conflict resolution_OEMConfig of Android Enterprise work profile");
            
            // Call IPLogin function
            await _securityBaseline.IPLogin(Page);
            
            // Add your test implementation here
            
            Console.WriteLine("Test completed successfully!");
        }
    }
}
