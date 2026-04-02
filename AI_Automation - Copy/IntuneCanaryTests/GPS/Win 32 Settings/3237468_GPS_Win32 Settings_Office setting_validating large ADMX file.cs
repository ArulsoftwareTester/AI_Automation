using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T3237468_GPS_Win32_Settings_Office_setting_validating_large_ADMX_file : PageTest
    {
        [Test]
        public async Task Test_3237468_GPS_Win32_Settings_Office_setting_validating_large_ADMX_file()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_3237468 completed");
        }
    }
}
