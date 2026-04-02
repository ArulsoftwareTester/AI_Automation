using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778158_DC_Device_Restrictions_Control_Panel_and_Settings_Devices
    {
        [Test]
        public async Task Test_8778158_DC_Device_Restrictions_Control_Panel_and_Settings_Devices()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778158 completed");
        }
    }
}
