using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T3113950_GPS_Reporting_Conflicting_policies : PageTest
    {
        [Test]
        public async Task Test_3113950_GPS_Reporting_Conflicting_policies()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_3113950 completed");
        }
    }
}
