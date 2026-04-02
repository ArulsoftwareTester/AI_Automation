using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15019526_E2E_Custom_Ingestion_Google___Windows___Chrome_ADMX_Package : PageTest
    {
        [Test]
        public async Task Test_15019526_E2E_Custom_Ingestion_Google___Windows___Chrome_ADMX_Package()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15019526 completed");
        }
    }
}
