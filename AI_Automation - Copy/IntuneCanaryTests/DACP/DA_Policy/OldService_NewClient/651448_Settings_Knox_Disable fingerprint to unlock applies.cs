using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class _651448_Settings_Knox_Disable_fingerprint_to_unlock_applies_OldSvcNewClient: PageTest
    {
        [Test]
        public async Task Test_651448_Settings_Knox_Disable_fingerprint_to_unlock_applies_OldSvcNewClient()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for Settings Knox Disable fingerprint to unlock
            Console.WriteLine("Test_651448: Settings_Knox_Disable fingerprint to unlock applies");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651448 completed");
        }
    }
}
