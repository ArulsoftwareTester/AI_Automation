using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767595_DC_Device_Restrictions_Start_Unpin_apps_from_task_bar
    {
        [Test]
        public async Task Test_8767595_DC_Device_Restrictions_Start_Unpin_apps_from_task_bar()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767595 completed");
        }
    }
}
