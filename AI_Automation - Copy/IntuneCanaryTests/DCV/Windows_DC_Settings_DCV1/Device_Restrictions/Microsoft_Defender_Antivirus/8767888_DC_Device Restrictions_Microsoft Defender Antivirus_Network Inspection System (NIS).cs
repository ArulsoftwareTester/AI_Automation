using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767888_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Network_Inspection_System_NIS_
    {
        [Test]
        public async Task Test_8767888_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Network_Inspection_System_NIS_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767888 completed");
        }
    }
}
