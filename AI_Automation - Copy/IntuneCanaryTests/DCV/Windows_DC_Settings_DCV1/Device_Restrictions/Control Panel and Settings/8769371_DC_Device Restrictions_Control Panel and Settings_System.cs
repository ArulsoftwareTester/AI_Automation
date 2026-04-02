using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8769371_DC_Device_Restrictions_Control_Panel_and_Settings_System
    {
        [Test]
        public async Task Test_8769371_DC_Device_Restrictions_Control_Panel_and_Settings_System()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8769371 completed");
        }
    }
}
