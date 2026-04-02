using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T3113553_GPS_Reporting_Success_status___Win32_setting : PageTest
    {
        [Test]
        public async Task Test_3113553_GPS_Reporting_Success_status___Win32_setting()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_3113553 completed");
        }
    }
}
