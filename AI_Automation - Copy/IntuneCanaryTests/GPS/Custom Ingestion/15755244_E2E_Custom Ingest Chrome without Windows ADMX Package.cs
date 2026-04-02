using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15755244_E2E_Custom_Ingest_Chrome_without_Windows_ADMX_Package : PageTest
    {
        [Test]
        public async Task Test_15755244_E2E_Custom_Ingest_Chrome_without_Windows_ADMX_Package()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15755244 completed");
        }
    }
}
