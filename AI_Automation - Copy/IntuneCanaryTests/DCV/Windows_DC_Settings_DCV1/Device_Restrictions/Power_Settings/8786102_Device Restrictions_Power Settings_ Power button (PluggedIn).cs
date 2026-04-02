using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8786102_Device_Restrictions_Power_Settings_Power_button_PluggedIn_
    {
        [Test]
        public async Task Test_8786102_Device_Restrictions_Power_Settings_Power_button_PluggedIn_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8786102 completed");
        }
    }
}
