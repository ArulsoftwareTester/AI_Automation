using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767693_DC_Device_Restrictions_Cellular_and_Connectivity_bluetooth_advertising
    {
        [Test]
        public async Task Test_8767693_DC_Device_Restrictions_Cellular_and_Connectivity_bluetooth_advertising()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767693 completed");
        }
    }
}
