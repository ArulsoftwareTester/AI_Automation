using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8787562_DC_Device_Restrictions_AppStore_install_app_data_on_system_volume
    {
        [Test]
        public async Task Test_8787562_DC_Device_Restrictions_AppStore_install_app_data_on_system_volume()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8787562 completed");
        }
    }
}
