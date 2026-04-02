using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class _651433_Settings_Knox_Disable_messaging_applies_OldSvcNewClient: PageTest
    {
        [Test]
        public async Task Test_651433_Settings_Knox_Disable_messaging_applies_OldSvcNewClient()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for Settings Knox Disable messaging
            Console.WriteLine("Test_651433: Settings_Knox_Disable messaging applies");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651433 completed");
        }
    }
}
