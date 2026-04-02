using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925802_Endpoint_Protection_LocalDeviceSecurityOptions_Devices_Format_and_eject_removable_media : SecurityBaseline
    {
        [Test]
        public async Task Test_8925802_Endpoint_Protection_LocalDeviceSecurityOptions_Devices_Format_and_eject_removable_media()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925802 completed");
        }
    }
}
