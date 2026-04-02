using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class _651427_Settings_Knox_Disable_copy_and_paste_applies_NewSvcNewClient: PageTest
    {
        [Test]
        public async Task Test_651427_Settings_Knox_Disable_copy_and_paste_applies_NewSvcNewClient()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for Settings Knox Disable copy and paste
            Console.WriteLine("Test_651427: Settings_Knox_Disable copy and paste applies");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651427 completed");
        }
    }
}
