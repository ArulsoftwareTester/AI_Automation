using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767916_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Scan_all_downloads
    {
        [Test]
        public async Task Test_8767916_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Scan_all_downloads()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767916 completed");
        }
    }
}
