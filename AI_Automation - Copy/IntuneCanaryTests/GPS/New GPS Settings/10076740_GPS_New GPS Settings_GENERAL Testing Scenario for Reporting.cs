using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T10076740_GPS_New_GPS_Settings_GENERAL_Testing_Scenario_for_Reporting : PageTest
    {
        [Test]
        public async Task Test_10076740_GPS_New_GPS_Settings_GENERAL_Testing_Scenario_for_Reporting()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_10076740 completed");
        }
    }
}
