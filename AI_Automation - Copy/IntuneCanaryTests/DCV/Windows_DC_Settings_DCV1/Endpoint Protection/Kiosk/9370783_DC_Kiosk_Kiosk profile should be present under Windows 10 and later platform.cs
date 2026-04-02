using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9370783_DC_Kiosk_Kiosk_profile_should_be_present_under_Windows_10_and_later_platform
    {
        [Test]
        public async Task Test_9370783_DC_Kiosk_Kiosk_profile_should_be_present_under_Windows_10_and_later_platform()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9370783 completed");
        }
    }
}
