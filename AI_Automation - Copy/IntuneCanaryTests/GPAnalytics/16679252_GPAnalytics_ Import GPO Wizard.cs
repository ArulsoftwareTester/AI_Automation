using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16679252_GPAnalytics__Import_GPO_Wizard : PageTest
    {
        [Test]
        public async Task Test_16679252_GPAnalytics__Import_GPO_Wizard()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16679252 completed");
        }
    }
}
