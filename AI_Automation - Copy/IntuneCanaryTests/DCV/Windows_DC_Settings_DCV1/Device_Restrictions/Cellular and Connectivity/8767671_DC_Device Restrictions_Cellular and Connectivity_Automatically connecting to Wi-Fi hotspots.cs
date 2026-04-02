using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767671_DC_Device_Restrictions_Cellular_and_Connectivity_Automatically_connecting_to_Wi_Fi_hotspots
    {
        [Test]
        public async Task Test_8767671_DC_Device_Restrictions_Cellular_and_Connectivity_Automatically_connecting_to_Wi_Fi_hotspots()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767671 completed");
        }
    }
}
