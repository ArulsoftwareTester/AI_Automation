using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906554_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Firewall_and_network_protection
    {
        [Test]
        public async Task Test_8906554_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Firewall_and_network_protection()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906554 completed");
        }
    }
}
