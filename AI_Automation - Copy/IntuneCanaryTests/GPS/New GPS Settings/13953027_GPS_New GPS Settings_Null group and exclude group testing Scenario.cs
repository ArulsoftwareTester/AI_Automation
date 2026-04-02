using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T13953027_GPS_New_GPS_Settings_Null_group_and_exclude_group_testing_Scenario : PageTest
    {
        [Test]
        public async Task Test_13953027_GPS_New_GPS_Settings_Null_group_and_exclude_group_testing_Scenario()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_13953027 completed");
        }
    }
}
