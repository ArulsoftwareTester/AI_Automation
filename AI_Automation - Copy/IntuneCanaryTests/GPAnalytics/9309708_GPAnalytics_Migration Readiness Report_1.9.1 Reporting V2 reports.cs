using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9309708_GPAnalytics_Migration_Readiness_Report_1_9_1_Reporting_V2_reports : PageTest
    {
        [Test]
        public async Task Test_9309708_GPAnalytics_Migration_Readiness_Report_1_9_1_Reporting_V2_reports()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9309708 completed");
        }
    }
}
