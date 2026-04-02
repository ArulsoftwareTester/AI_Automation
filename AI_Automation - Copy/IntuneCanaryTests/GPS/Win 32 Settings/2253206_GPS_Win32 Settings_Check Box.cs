using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2253206_GPS_Win32_Settings_Check_Box : PageTest
    {
        [Test]
        public async Task Test_2253206_GPS_Win32_Settings_Check_Box()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2253206 completed");
        }
    }
}
