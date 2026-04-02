using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2360948_GPS_OS_Setting_with_checkbox : PageTest
    {
        [Test]
        public async Task Test_2360948_GPS_OS_Setting_with_checkbox()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2360948 completed");
        }
    }
}
