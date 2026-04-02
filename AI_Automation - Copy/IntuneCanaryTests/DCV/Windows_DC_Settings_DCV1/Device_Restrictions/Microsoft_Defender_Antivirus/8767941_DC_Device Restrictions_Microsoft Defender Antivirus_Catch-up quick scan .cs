using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767941_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Catch_up_quick_scan
    {
        [Test]
        public async Task Test_8767941_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Catch_up_quick_scan()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767941 completed");
        }
    }
}
