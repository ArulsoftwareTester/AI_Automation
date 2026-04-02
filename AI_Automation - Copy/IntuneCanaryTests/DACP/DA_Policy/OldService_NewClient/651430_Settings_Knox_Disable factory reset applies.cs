using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class _651430_Settings_Knox_Disable_factory_reset_applies_OldSvcNewClient: PageTest
    {
        [Test]
        public async Task Test_651430_Settings_Knox_Disable_factory_reset_applies_OldSvcNewClient()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for Settings Knox Disable factory reset
            Console.WriteLine("Test_651430: Settings_Knox_Disable factory reset applies");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651430 completed");
        }
    }
}
