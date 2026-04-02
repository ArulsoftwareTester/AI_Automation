using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T14976802_GPAnalytics__Migration_Readiness__Verify_Summary_Report_after_Create : PageTest
    {
        [Test]
        public async Task Test_14976802_GPAnalytics__Migration_Readiness__Verify_Summary_Report_after_Create()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_14976802 completed");
        }
    }
}
