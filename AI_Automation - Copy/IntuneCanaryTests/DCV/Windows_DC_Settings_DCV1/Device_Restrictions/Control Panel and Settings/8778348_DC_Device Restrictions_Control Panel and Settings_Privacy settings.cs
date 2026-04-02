using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778348_DC_Device_Restrictions_Control_Panel_and_Settings_Privacy_settings
    {
        [Test]
        public async Task Test_8778348_DC_Device_Restrictions_Control_Panel_and_Settings_Privacy_settings()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778348 completed");
        }
    }
}
