using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853037_GPS_UX_Test_Properties__Report__View_report : PageTest
    {
        [Test]
        public async Task Test_26853037_GPS_UX_Test_Properties__Report__View_report()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853037 completed");
        }
    }
}
