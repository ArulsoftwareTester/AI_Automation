using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2253743_GPS_Win32_Settings_Enabled : PageTest
    {
        [Test]
        public async Task Test_2253743_GPS_Win32_Settings_Enabled()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2253743 completed");
        }
    }
}
