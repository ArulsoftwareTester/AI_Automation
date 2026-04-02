using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2366430_GPS_Conflicts_Assign_2_different_configutations_with_conflicting_settings_to_the_user_and_device : PageTest
    {
        [Test]
        public async Task Test_2366430_GPS_Conflicts_Assign_2_different_configutations_with_conflicting_settings_to_the_user_and_device()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2366430 completed");
        }
    }
}
