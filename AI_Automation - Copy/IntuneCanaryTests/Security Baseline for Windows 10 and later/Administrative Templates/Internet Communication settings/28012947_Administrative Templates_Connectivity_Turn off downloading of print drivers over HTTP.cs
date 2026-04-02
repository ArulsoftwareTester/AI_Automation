using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28012947_Administrative_Templates_Connectivity_Turn_off_downloading_of_print_drivers_over_HTTP : PageTest
    {
        [Test]
        public async Task Test_28012947_Administrative_Templates_Connectivity_Turn_off_downloading_of_print_drivers_over_HTTP()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later");
            await securityBaseline.MDMPolicySync(Page);
            Console.WriteLine("Test_28012947 completed");
        }
    }
}
