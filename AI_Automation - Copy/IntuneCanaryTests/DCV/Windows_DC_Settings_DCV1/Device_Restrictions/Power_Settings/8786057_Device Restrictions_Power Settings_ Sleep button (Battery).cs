using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8786057_Device_Restrictions_Power_Settings_Sleep_button_Battery_
    {
        [Test]
        public async Task Test_8786057_Device_Restrictions_Power_Settings_Sleep_button_Battery_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8786057 completed");
        }
    }
}
