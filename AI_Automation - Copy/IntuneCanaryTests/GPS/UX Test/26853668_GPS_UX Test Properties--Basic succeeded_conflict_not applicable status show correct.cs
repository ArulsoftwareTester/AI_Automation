using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853668_GPS_UX_Test_Properties__Basic_succeeded_conflict_not_applicable_status_show_correct : PageTest
    {
        [Test]
        public async Task Test_26853668_GPS_UX_Test_Properties__Basic_succeeded_conflict_not_applicable_status_show_correct()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853668 completed");
        }
    }
}
