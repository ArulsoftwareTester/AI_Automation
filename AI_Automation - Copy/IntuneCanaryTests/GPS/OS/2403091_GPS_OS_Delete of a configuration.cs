using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2403091_GPS_OS_Delete_of_a_configuration : PageTest
    {
        [Test]
        public async Task Test_2403091_GPS_OS_Delete_of_a_configuration()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2403091 completed");
        }
    }
}
