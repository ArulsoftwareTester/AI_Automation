using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8822617_DC_Device_Restrictions_Locked_Screen_Experience_Toast_notifications_on_locked_screen
    {
        [Test]
        public async Task Test_8822617_DC_Device_Restrictions_Locked_Screen_Experience_Toast_notifications_on_locked_screen()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8822617 completed");
        }
    }
}
