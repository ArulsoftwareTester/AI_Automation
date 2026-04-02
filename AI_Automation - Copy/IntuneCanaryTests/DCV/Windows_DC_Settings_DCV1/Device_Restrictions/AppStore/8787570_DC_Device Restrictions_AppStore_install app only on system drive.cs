using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8787570_DC_Device_Restrictions_AppStore_install_app_only_on_system_drive
    {
        [Test]
        public async Task Test_8787570_DC_Device_Restrictions_AppStore_install_app_only_on_system_drive()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8787570 completed");
        }
    }
}
