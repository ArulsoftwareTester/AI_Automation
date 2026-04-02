using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925723_Endpoint_Protection_LocalDeviceSecurityOptions_Devices_Install_printer_drivers_for_shared_printers : SecurityBaseline
    {
        [Test]
        public async Task Test_8925723_Endpoint_Protection_LocalDeviceSecurityOptions_Devices_Install_printer_drivers_for_shared_printers()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925723 completed");
        }
    }
}
