using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778302_DC_Device_Restrictions_Control_Panel_and_Settings_System_time_modification
    {
        [Test]
        public async Task Test_8778302_DC_Device_Restrictions_Control_Panel_and_Settings_System_time_modification()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778302 completed");
        }
    }
}
