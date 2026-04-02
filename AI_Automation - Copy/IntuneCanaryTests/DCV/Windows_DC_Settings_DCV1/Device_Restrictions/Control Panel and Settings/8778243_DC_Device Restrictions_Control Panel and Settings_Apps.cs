using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778243_DC_Device_Restrictions_Control_Panel_and_Settings_Apps
    {
        [Test]
        public async Task Test_8778243_DC_Device_Restrictions_Control_Panel_and_Settings_Apps()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778243 completed");
        }
    }
}
