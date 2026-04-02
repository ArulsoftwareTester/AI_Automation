using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8822336_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Scan_archive_file
    {
        [Test]
        public async Task Test_8822336_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Scan_archive_file()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8822336 completed");
        }
    }
}
