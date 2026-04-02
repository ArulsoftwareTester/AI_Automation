using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16679243_GPAnalytics_Wizard_Scoped_Admin_Create : PageTest
    {
        [Test]
        public async Task Test_16679243_GPAnalytics_Wizard_Scoped_Admin_Create()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16679243 completed");
        }
    }
}
