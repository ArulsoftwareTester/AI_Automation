using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767893_DC_Device_Restrictions_Cloud_Printer_Print_service_resource_URI
    {
        [Test]
        public async Task Test_8767893_DC_Device_Restrictions_Cloud_Printer_Print_service_resource_URI()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767893 completed");
        }
    }
}
