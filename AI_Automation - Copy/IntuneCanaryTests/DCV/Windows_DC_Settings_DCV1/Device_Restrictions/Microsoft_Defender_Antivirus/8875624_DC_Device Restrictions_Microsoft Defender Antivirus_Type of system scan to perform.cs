using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8875624_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Type_of_system_scan_to_perform
    {
        [Test]
        public async Task Test_8875624_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Type_of_system_scan_to_perform()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8875624 completed");
        }
    }
}
