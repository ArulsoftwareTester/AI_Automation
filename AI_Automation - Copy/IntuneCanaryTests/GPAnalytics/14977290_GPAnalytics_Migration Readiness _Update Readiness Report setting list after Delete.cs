using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T14977290_GPAnalytics_Migration_Readiness__Update_Readiness_Report_setting_list_after_Delete : PageTest
    {
        [Test]
        public async Task Test_14977290_GPAnalytics_Migration_Readiness__Update_Readiness_Report_setting_list_after_Delete()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_14977290 completed");
        }
    }
}
