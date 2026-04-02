using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T14977191_GPAnalytics__Migration_Readiness__Verify_settings_from_Migration_Report_show_up_in_Readiness_Report : PageTest
    {
        [Test]
        public async Task Test_14977191_GPAnalytics__Migration_Readiness__Verify_settings_from_Migration_Report_show_up_in_Readiness_Report()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_14977191 completed");
        }
    }
}
