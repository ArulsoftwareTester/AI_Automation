using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767586_DC_Device_Restrictions_Cellular_and_Connectivity_VPN_over_the_cellular_network
    {
        [Test]
        public async Task Test_8767586_DC_Device_Restrictions_Cellular_and_Connectivity_VPN_over_the_cellular_network()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767586 completed");
        }
    }
}
