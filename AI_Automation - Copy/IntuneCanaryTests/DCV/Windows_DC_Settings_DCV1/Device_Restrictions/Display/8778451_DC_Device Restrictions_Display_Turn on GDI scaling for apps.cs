using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778451_DC_Device_Restrictions_Display_Turn_on_GDI_scaling_for_apps
    {
        [Test]
        public async Task Test_8778451_DC_Device_Restrictions_Display_Turn_on_GDI_scaling_for_apps()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778451 completed");
        }
    }
}
