using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853019_GPS_UX_Test_Properties___Delete : PageTest
    {
        [Test]
        public async Task Test_26853019_GPS_UX_Test_Properties___Delete()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853019 completed");
        }
    }
}
