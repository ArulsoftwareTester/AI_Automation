using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8993830_DC_Device_Restrictions_AppStore_Apps_from_store_only
    {
        [Test]
        public async Task Test_8993830_DC_Device_Restrictions_AppStore_Apps_from_store_only()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8993830 completed");
        }
    }
}
