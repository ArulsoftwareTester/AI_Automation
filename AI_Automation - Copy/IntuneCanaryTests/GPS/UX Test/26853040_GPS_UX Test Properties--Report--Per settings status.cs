using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853040_GPS_UX_Test_Properties__Report__Per_settings_status : PageTest
    {
        [Test]
        public async Task Test_26853040_GPS_UX_Test_Properties__Report__Per_settings_status()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853040 completed");
        }
    }
}
