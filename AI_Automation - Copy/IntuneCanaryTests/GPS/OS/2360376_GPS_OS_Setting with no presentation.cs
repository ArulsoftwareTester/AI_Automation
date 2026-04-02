using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2360376_GPS_OS_Setting_with_no_presentation : PageTest
    {
        [Test]
        public async Task Test_2360376_GPS_OS_Setting_with_no_presentation()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2360376 completed");
        }
    }
}
