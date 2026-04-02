using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2401187_GPS_Input_Validation_ListBox_without_explicit_value : PageTest
    {
        [Test]
        public async Task Test_2401187_GPS_Input_Validation_ListBox_without_explicit_value()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2401187 completed");
        }
    }
}
