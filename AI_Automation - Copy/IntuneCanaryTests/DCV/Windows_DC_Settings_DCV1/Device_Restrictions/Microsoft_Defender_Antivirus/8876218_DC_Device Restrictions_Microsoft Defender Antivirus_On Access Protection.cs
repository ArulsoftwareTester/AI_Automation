using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8876218_DC_Device_Restrictions_Microsoft_Defender_Antivirus_On_Access_Protection
    {
        [Test]
        public async Task Test_8876218_DC_Device_Restrictions_Microsoft_Defender_Antivirus_On_Access_Protection()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8876218 completed");
        }
    }
}
