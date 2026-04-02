using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2304310_GPS_Win32_Settings_Drop_down_List : PageTest
    {
        [Test]
        public async Task Test_2304310_GPS_Win32_Settings_Drop_down_List()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2304310 completed");
        }
    }
}
