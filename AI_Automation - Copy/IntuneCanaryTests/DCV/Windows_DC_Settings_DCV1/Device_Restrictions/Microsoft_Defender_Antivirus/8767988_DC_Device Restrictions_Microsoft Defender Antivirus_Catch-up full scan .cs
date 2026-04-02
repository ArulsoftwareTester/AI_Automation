using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767988_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Catch_up_full_scan
    {
        [Test]
        public async Task Test_8767988_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Catch_up_full_scan()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767988 completed");
        }
    }
}
