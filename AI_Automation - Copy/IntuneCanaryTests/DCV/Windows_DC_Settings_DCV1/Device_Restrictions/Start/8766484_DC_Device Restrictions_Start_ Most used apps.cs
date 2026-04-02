using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766484_DC_Device_Restrictions_Start_Most_used_apps
    {
        [Test]
        public async Task Test_8766484_DC_Device_Restrictions_Start_Most_used_apps()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766484 completed");
        }
    }
}
