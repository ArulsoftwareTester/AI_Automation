using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778221_DC_Device_Restrictions_Control_Panel_and_Settings_Network_and_Internet
    {
        [Test]
        public async Task Test_8778221_DC_Device_Restrictions_Control_Panel_and_Settings_Network_and_Internet()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778221 completed");
        }
    }
}
