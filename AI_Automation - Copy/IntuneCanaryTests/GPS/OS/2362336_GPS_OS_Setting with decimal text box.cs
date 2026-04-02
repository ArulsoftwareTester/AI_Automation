using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2362336_GPS_OS_Setting_with_decimal_text_box : PageTest
    {
        [Test]
        public async Task Test_2362336_GPS_OS_Setting_with_decimal_text_box()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2362336 completed");
        }
    }
}
