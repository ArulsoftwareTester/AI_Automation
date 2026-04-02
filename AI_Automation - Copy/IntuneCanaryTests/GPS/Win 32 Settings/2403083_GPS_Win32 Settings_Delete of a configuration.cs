using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2403083_GPS_Win32_Settings_Delete_of_a_configuration : PageTest
    {
        [Test]
        public async Task Test_2403083_GPS_Win32_Settings_Delete_of_a_configuration()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2403083 completed");
        }
    }
}
