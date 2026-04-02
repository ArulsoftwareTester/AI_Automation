using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767085_DC_Device_Restrictions_Start_Recently_added_apps
    {
        [Test]
        public async Task Test_8767085_DC_Device_Restrictions_Start_Recently_added_apps()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767085 completed");
        }
    }
}
