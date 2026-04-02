using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8769252_DC_Device_Restrictions_Locked_Screen_Experience_Locked_screen_picture_URL_Desktop_only
    {
        [Test]
        public async Task Test_8769252_DC_Device_Restrictions_Locked_Screen_Experience_Locked_screen_picture_URL_Desktop_only()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8769252 completed");
        }
    }
}
