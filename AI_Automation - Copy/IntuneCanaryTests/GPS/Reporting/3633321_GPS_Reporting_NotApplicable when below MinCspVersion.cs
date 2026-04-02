using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T3633321_GPS_Reporting_NotApplicable_when_below_MinCspVersion : PageTest
    {
        [Test]
        public async Task Test_3633321_GPS_Reporting_NotApplicable_when_below_MinCspVersion()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_3633321 completed");
        }
    }
}
