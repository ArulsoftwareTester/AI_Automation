using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766651_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Real_time_monitoring
    {
        [Test]
        public async Task Test_8766651_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Real_time_monitoring()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766651 completed");
        }
    }
}
