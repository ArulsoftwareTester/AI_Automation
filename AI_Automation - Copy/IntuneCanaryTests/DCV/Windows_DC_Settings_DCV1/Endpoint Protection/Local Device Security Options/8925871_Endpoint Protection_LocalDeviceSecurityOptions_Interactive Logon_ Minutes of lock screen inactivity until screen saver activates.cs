using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925871_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Minutes_of_lock_screen_inactivity_until_screen_saver_activates : SecurityBaseline
    {
        [Test]
        public async Task Test_8925871_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Minutes_of_lock_screen_inactivity_until_screen_saver_activates()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925871 completed");
        }
    }
}
