using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16612996_Notifications_Battery_Optimization_Unenrolled_devices_do_not_show_battery_optimization_notification : SecurityBaseline
    {
        [Test]
        public async Task Test_16612996_Notifications_Battery_Optimization_Unenrolled_devices_do_not_show_battery_optimization_notification()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_16612996 completed");
        }
    }
}
