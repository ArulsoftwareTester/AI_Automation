using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T3113533_GPS_Reporting_Success_status___System_settings : PageTest
    {
        [Test]
        public async Task Test_3113533_GPS_Reporting_Success_status___System_settings()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_3113533 completed");
        }
    }
}
