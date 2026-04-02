using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2364456_GPS_Conflicts_Conflicting_win32_settings_to_the_same_group : PageTest
    {
        [Test]
        public async Task Test_2364456_GPS_Conflicts_Conflicting_win32_settings_to_the_same_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2364456 completed");
        }
    }
}
