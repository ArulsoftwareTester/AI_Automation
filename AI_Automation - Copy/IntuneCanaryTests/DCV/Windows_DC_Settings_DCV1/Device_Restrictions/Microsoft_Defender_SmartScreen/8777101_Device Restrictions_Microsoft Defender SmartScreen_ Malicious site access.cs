using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8777101_Device_Restrictions_Microsoft_Defender_SmartScreen_Malicious_site_access
    {
        [Test]
        public async Task Test_8777101_Device_Restrictions_Microsoft_Defender_SmartScreen_Malicious_site_access()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8777101 completed");
        }
    }
}
