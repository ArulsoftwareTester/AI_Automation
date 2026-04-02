using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853039_GPS_UX_Test_Properties__Report__Device_assignment_status : PageTest
    {
        [Test]
        public async Task Test_26853039_GPS_UX_Test_Properties__Report__Device_assignment_status()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853039 completed");
        }
    }
}
