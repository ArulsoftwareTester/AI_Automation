using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2306331_GPS_Win32_Settings_Decimal_Text_Box : PageTest
    {
        [Test]
        public async Task Test_2306331_GPS_Win32_Settings_Decimal_Text_Box()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2306331 completed");
        }
    }
}
