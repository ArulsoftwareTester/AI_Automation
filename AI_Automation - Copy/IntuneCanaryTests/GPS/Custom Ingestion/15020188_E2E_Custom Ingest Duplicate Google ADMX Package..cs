using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15020188_E2E_Custom_Ingest_Duplicate_Google_ADMX_Package_ : PageTest
    {
        [Test]
        public async Task Test_15020188_E2E_Custom_Ingest_Duplicate_Google_ADMX_Package_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15020188 completed");
        }
    }
}
