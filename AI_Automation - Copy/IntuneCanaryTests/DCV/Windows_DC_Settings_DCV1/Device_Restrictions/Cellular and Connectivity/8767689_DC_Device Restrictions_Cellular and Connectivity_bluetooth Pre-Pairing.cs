using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767689_DC_Device_Restrictions_Cellular_and_Connectivity_bluetooth_Pre_Pairing
    {
        [Test]
        public async Task Test_8767689_DC_Device_Restrictions_Cellular_and_Connectivity_bluetooth_Pre_Pairing()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767689 completed");
        }
    }
}
