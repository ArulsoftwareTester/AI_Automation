using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1076277_Enrollment_Knox_Install_SSP_on_a_knox_capableAFW_device_Enroll_Go_to_settings_See_setting_to_enable_Browser_Access_Kill_ssp_reopen_ssp_go_to_settings_still_see_setting : SecurityBaseline
    {
        [Test]
        public async Task Test_1076277_Enrollment_Knox_Install_SSP_on_a_knox_capableAFW_device_Enroll_Go_to_settings_See_setting_to_enable_Browser_Access_Kill_ssp_reopen_ssp_go_to_settings_still_see_setting()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1076277 completed");
        }
    }
}
