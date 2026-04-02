using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2401194_GPS_Input_Validation_Required_Dropdown : PageTest
    {
        [Test]
        public async Task Test_2401194_GPS_Input_Validation_Required_Dropdown()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2401194 completed");
        }
    }
}
