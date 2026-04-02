using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766900_DC_Device_Restrictions_Cloud_Printer_Printer_discovery_URL
    {
        [Test]
        public async Task Test_8766900_DC_Device_Restrictions_Cloud_Printer_Printer_discovery_URL()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766900 completed");
        }
    }
}
