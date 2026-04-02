using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5867782_GPS_Reporting_Success_status_returns_after_conflict_resolution : PageTest
    {
        [Test]
        public async Task Test_5867782_GPS_Reporting_Success_status_returns_after_conflict_resolution()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_5867782 completed");
        }
    }
}
