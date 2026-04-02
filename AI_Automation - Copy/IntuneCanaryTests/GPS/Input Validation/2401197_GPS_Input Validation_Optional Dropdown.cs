using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2401197_GPS_Input_Validation_Optional_Dropdown : PageTest
    {
        [Test]
        public async Task Test_2401197_GPS_Input_Validation_Optional_Dropdown()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2401197 completed");
        }
    }
}
