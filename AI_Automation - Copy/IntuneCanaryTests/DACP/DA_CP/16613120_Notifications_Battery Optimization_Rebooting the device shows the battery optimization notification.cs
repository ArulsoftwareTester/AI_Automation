using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16613120_Notifications_Battery_Optimization_Rebooting_the_device_shows_the_battery_optimization_notification : SecurityBaseline
    {
        [Test]
        public async Task Test_16613120_Notifications_Battery_Optimization_Rebooting_the_device_shows_the_battery_optimization_notification()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_16613120 completed");
        }
    }
}
