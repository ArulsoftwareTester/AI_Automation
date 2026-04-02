using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925770_Endpoint_Protection_LocalDeviceSecurityOptions_Devices_Restrict_CDROM_access_to_local_active_user : SecurityBaseline
    {
        [Test]
        public async Task Test_8925770_Endpoint_Protection_LocalDeviceSecurityOptions_Devices_Restrict_CDROM_access_to_local_active_user()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925770 completed");
        }
    }
}
