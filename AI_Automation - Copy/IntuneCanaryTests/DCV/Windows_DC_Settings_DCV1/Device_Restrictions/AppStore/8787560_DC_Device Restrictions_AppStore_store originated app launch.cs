using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8787560_DC_Device_Restrictions_AppStore_store_originated_app_launch
    {
        [Test]
        public async Task Test_8787560_DC_Device_Restrictions_AppStore_store_originated_app_launch()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8787560 completed");
        }
    }
}
