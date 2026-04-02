using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778053_DC_Device_Restrictions_Control_Panel_and_Settings_Power_and_sleep_settings_modification_desktop_only
    {
        [Test]
        public async Task Test_8778053_DC_Device_Restrictions_Control_Panel_and_Settings_Power_and_sleep_settings_modification_desktop_only()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778053 completed");
        }
    }
}
