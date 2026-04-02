using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8787590_DC_Device_Restrictions_AppStore_install_apps_with_elevated_privileges
    {
        [Test]
        public async Task Test_8787590_DC_Device_Restrictions_AppStore_install_apps_with_elevated_privileges()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8787590 completed");
        }
    }
}
