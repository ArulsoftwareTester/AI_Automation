using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class _651425_Settings_Knox_Disable_voice_assistant_applies_OldSvcNewClient: PageTest
    {
        [Test]
        public async Task Test_651425_Settings_Knox_Disable_voice_assistant_applies_OldSvcNewClient()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for Settings Knox Disable voice assistant
            Console.WriteLine("Test_651425: Settings_Knox_Disable voice assistant applies");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651425 completed");
        }
    }
}
