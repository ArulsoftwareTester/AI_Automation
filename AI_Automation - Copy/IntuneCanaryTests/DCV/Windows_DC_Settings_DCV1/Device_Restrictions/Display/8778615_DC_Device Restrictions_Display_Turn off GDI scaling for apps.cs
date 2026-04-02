using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778615_DC_Device_Restrictions_Display_Turn_off_GDI_scaling_for_apps
    {
        [Test]
        public async Task Test_8778615_DC_Device_Restrictions_Display_Turn_off_GDI_scaling_for_apps()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778615 completed");
        }
    }
}
