using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2364376_GPS_OS_Setting_with_presentation_text_and_multiple_presentations : PageTest
    {
        [Test]
        public async Task Test_2364376_GPS_OS_Setting_with_presentation_text_and_multiple_presentations()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2364376 completed");
        }
    }
}
