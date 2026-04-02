using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853015_GPS_UX_Test_Properties___Create : PageTest
    {
        [Test]
        public async Task Test_26853015_GPS_UX_Test_Properties___Create()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853015 completed");
        }
    }
}
