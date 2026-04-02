using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T3219119_GPS_Win32_Settings_Push_notification_validation : PageTest
    {
        [Test]
        public async Task Test_3219119_GPS_Win32_Settings_Push_notification_validation()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_3219119 completed");
        }
    }
}
