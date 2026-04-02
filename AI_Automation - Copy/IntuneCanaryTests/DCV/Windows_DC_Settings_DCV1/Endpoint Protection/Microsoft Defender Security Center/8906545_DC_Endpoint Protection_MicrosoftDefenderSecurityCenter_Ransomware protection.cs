using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906545_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Ransomware_protection
    {
        [Test]
        public async Task Test_8906545_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Ransomware_protection()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906545 completed");
        }
    }
}
