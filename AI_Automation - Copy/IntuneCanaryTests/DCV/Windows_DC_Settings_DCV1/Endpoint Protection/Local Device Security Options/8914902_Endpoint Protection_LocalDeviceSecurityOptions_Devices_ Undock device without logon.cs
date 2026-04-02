using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8914902_Endpoint_Protection_LocalDeviceSecurityOptions_Devices_Undock_device_without_logon : SecurityBaseline
    {
        [Test]
        public async Task Test_8914902_Endpoint_Protection_LocalDeviceSecurityOptions_Devices_Undock_device_without_logon()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8914902 completed");
        }
    }
}
