using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778289_DC_Device_Restrictions_Control_Panel_and_Settings_Time_and_Language
    {
        [Test]
        public async Task Test_8778289_DC_Device_Restrictions_Control_Panel_and_Settings_Time_and_Language()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8778289 completed");
        }
    }
}
