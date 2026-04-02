using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2401198_GPS_Input_Validation_TextBox_Length : PageTest
    {
        [Test]
        public async Task Test_2401198_GPS_Input_Validation_TextBox_Length()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2401198 completed");
        }
    }
}
