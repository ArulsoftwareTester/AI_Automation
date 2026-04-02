using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8787586_DC_Device_Restrictions_AppStore_user_control_over_installations
    {
        [Test]
        public async Task Test_8787586_DC_Device_Restrictions_AppStore_user_control_over_installations()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8787586 completed");
        }
    }
}
