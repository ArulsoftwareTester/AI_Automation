using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2360851_GPS_OS_Setting_with_textbox : PageTest
    {
        [Test]
        public async Task Test_2360851_GPS_OS_Setting_with_textbox()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2360851 completed");
        }
    }
}
