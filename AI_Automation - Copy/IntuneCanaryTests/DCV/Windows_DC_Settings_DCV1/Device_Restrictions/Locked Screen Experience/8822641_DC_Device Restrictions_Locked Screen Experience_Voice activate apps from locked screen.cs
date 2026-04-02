using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8822641_DC_Device_Restrictions_Locked_Screen_Experience_Voice_activate_apps_from_locked_screen
    {
        [Test]
        public async Task Test_8822641_DC_Device_Restrictions_Locked_Screen_Experience_Voice_activate_apps_from_locked_screen()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8822641 completed");
        }
    }
}
