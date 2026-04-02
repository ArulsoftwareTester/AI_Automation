using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8875585_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Time_to_perform_a_daily_quick_scan
    {
        [Test]
        public async Task Test_8875585_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Time_to_perform_a_daily_quick_scan()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8875585 completed");
        }
    }
}
