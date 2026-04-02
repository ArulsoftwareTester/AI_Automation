using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853034_GPS_UX_Test_Properties___Report__Checkin_status : PageTest
    {
        [Test]
        public async Task Test_26853034_GPS_UX_Test_Properties___Report__Checkin_status()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853034 completed");
        }
    }
}
