using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2401189_GPS_Input_Validation_Required_DecimalTextBox : PageTest
    {
        [Test]
        public async Task Test_2401189_GPS_Input_Validation_Required_DecimalTextBox()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2401189 completed");
        }
    }
}
