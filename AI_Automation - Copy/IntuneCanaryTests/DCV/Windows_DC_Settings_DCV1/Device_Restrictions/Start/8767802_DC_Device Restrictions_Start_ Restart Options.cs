using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767802_DC_Device_Restrictions_Start_Restart_Options
    {
        [Test]
        public async Task Test_8767802_DC_Device_Restrictions_Start_Restart_Options()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767802 completed");
        }
    }
}
