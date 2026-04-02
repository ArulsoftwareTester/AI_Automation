using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766373_DC_Device_Restrictions_AppStore_Trusted_app_installation
    {
        [Test]
        public async Task Test_8766373_DC_Device_Restrictions_AppStore_Trusted_app_installation()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766373 completed");
        }
    }
}
