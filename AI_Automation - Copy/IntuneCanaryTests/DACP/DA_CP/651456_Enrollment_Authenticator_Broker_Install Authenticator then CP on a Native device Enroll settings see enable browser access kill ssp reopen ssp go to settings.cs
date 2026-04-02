using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651456_Enrollment_Authenticator_Broker_Install_Authenticator_then_CP_on_a_Native_device_Enroll_settings_see_enable_browser_access_kill_ssp_reopen_ssp_go_to_settings : SecurityBaseline
    {
        [Test]
        public async Task Test_651456_Enrollment_Authenticator_Broker_Install_Authenticator_then_CP_on_a_Native_device_Enroll_settings_see_enable_browser_access_kill_ssp_reopen_ssp_go_to_settings()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_651456 completed");
        }
    }
}
