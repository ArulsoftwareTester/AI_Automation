using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2361184_GPS_OS_Setting_with_dropdown : PageTest
    {
        [Test]
        public async Task Test_2361184_GPS_OS_Setting_with_dropdown()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2361184 completed");
        }
    }
}
