using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class _651452_Settings_KNOX_Prevent_some_apps_from_launching_applies_OldSvcNewClient: PageTest
    {
        [Test]
        public async Task Test_651452_Settings_KNOX_Prevent_some_apps_from_launching_applies_OldSvcNewClient()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for Settings KNOX Prevent some apps from launching applies
            Console.WriteLine("Test_651452: Settings_KNOX_Prevent some apps from launching applies");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651452 completed");
        }
    }
}
