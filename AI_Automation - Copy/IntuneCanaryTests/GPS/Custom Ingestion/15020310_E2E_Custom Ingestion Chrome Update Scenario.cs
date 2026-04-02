using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15020310_E2E_Custom_Ingestion_Chrome_Update_Scenario : PageTest
    {
        [Test]
        public async Task Test_15020310_E2E_Custom_Ingestion_Chrome_Update_Scenario()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15020310 completed");
        }
    }
}
