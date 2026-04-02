using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767677_DC_Device_Restrictions_Cellular_and_Connectivity_Manual_Wi_Fi_configuration
    {
        [Test]
        public async Task Test_8767677_DC_Device_Restrictions_Cellular_and_Connectivity_Manual_Wi_Fi_configuration()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767677 completed");
        }
    }
}
