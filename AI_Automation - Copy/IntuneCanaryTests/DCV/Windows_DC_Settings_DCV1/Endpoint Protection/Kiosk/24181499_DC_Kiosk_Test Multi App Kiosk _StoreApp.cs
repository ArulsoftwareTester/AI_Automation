using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T24181499_DC_Kiosk_Test_Multi_App_Kiosk__StoreApp
    {
        [Test]
        public async Task Test_24181499_DC_Kiosk_Test_Multi_App_Kiosk__StoreApp()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_24181499 completed");
        }
    }
}
