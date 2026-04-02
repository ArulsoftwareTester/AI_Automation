using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8823240_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Cloud_delivered_protection
    {
        [Test]
        public async Task Test_8823240_DC_Device_Restrictions_Microsoft_Defender_Antivirus_Cloud_delivered_protection()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8823240 completed");
        }
    }
}
