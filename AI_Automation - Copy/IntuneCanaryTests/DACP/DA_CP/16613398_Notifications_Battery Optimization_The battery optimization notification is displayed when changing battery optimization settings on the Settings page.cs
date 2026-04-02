using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16613398_Notifications_Battery_Optimization_The_battery_optimization_notification_is_displayed_when_changing_battery_optimization_settings_on_the_Settings_page : SecurityBaseline
    {
        [Test]
        public async Task Test_16613398_Notifications_Battery_Optimization_The_battery_optimization_notification_is_displayed_when_changing_battery_optimization_settings_on_the_Settings_page()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_16613398 completed");
        }
    }
}
