using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767670_DC_Device_Restrictions_Start_Start_screen_mode
    {
        [Test]
        public async Task Test_8767670_DC_Device_Restrictions_Start_Start_screen_mode()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767670 completed");
        }
    }
}
