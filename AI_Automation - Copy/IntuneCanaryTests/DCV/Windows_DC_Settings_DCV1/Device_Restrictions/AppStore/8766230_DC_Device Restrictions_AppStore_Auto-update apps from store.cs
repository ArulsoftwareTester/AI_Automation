using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766230_DC_Device_Restrictions_AppStore_Auto_update_apps_from_store
    {
        [Test]
        public async Task Test_8766230_DC_Device_Restrictions_AppStore_Auto_update_apps_from_store()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766230 completed");
        }
    }
}
