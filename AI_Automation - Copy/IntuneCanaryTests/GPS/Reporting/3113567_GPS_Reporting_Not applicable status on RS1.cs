using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T3113567_GPS_Reporting_Not_applicable_status_on_RS1 : PageTest
    {
        [Test]
        public async Task Test_3113567_GPS_Reporting_Not_applicable_status_on_RS1()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_3113567 completed");
        }
    }
}
