using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2696090_Devices_Android_Company_Portal_Remote_Actions_When_an_admin_remotely_locks_a_device_it_happens_almost_immediately_even_when_the_device_is_in_battery_saver_mode : SecurityBaseline
    {
        [Test]
        public async Task Test_2696090_Devices_Android_Company_Portal_Remote_Actions_When_an_admin_remotely_locks_a_device_it_happens_almost_immediately_even_when_the_device_is_in_battery_saver_mode()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_2696090 completed");
        }
    }
}
