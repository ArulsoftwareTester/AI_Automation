using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9370804_DC_Kiosk_Kiosk_browser_settings_UI_under_kiosk_web_browser_category
    {
        [Test]
        public async Task Test_9370804_DC_Kiosk_Kiosk_browser_settings_UI_under_kiosk_web_browser_category()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9370804 completed");
        }
    }
}
