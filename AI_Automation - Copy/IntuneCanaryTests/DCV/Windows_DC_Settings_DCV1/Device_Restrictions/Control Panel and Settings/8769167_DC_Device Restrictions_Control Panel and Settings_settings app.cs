using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8769167_DC_Device_Restrictions_Control_Panel_and_Settings_settings_app
    {
        [Test]
        public async Task Test_8769167_DC_Device_Restrictions_Control_Panel_and_Settings_settings_app()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8769167 completed");
        }
    }
}
