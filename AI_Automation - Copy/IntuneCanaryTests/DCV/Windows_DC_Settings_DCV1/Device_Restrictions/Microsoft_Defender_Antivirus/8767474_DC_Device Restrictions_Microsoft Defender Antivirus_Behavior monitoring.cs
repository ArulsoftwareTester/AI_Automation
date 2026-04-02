using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767474_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Behavior_monitoring
    {
        [Test]
        public async Task Test_8767474_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Behavior_monitoring()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767474 completed");
        }
    }
}
