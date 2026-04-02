using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class _651421_Settings_Knox_Disable_power_off_applies_NewSvcNewClient: PageTest
    {
        [Test]
        public async Task Test_651421_Settings_Knox_Disable_power_off_applies_NewSvcNewClient()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for Settings Knox Disable power off
            Console.WriteLine("Test_651421: Settings_Knox_Disable power off applies");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651421 completed");
        }
    }
}
