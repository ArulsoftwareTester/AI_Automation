using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9309754_GPAnalytics_Migration_Readiness_Report_1_9_2 : PageTest
    {
        [Test]
        public async Task Test_9309754_GPAnalytics_Migration_Readiness_Report_1_9_2()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9309754 completed");
        }
    }
}
