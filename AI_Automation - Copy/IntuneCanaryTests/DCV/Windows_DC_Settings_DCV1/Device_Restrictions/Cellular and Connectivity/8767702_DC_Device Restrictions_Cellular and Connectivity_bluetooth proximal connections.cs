using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767702_DC_Device_Restrictions_Cellular_and_Connectivity_bluetooth_proximal_connections
    {
        [Test]
        public async Task Test_8767702_DC_Device_Restrictions_Cellular_and_Connectivity_bluetooth_proximal_connections()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767702 completed");
        }
    }
}
