using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T10004282_GPS_New_GPS_Settings_Versioning_Testing_Scenario : PageTest
    {
        [Test]
        public async Task Test_10004282_GPS_New_GPS_Settings_Versioning_Testing_Scenario()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_10004282 completed");
        }
    }
}
