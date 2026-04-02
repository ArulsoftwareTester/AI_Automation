using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767714_DC_Device_Restrictions_Start_Power_button
    {
        [Test]
        public async Task Test_8767714_DC_Device_Restrictions_Start_Power_button()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767714 completed");
        }
    }
}
