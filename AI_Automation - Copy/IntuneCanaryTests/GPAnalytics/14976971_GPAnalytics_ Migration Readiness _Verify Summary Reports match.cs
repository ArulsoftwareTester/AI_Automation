using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T14976971_GPAnalytics__Migration_Readiness__Verify_Summary_Reports_match : PageTest
    {
        [Test]
        public async Task Test_14976971_GPAnalytics__Migration_Readiness__Verify_Summary_Reports_match()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_14976971 completed");
        }
    }
}
