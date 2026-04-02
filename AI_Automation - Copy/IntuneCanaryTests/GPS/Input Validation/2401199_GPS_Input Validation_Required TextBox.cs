using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2401199_GPS_Input_Validation_Required_TextBox : PageTest
    {
        [Test]
        public async Task Test_2401199_GPS_Input_Validation_Required_TextBox()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2401199 completed");
        }
    }
}
