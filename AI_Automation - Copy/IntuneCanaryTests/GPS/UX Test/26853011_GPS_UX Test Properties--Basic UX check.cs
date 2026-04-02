using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853011_GPS_UX_Test_Properties__Basic_UX_check : PageTest
    {
        [Test]
        public async Task Test_26853011_GPS_UX_Test_Properties__Basic_UX_check()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853011 completed");
        }
    }
}
