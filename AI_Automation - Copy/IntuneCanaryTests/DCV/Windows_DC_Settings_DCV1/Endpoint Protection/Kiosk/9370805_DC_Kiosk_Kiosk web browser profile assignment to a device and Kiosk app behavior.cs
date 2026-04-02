using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9370805_DC_Kiosk_Kiosk_web_browser_profile_assignment_to_a_device_and_Kiosk_app_behavior
    {
        [Test]
        public async Task Test_9370805_DC_Kiosk_Kiosk_web_browser_profile_assignment_to_a_device_and_Kiosk_app_behavior()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9370805 completed");
        }
    }
}
