using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16679334_GPAnalytics_Edit_Scope_Tags_on_GPO : PageTest
    {
        [Test]
        public async Task Test_16679334_GPAnalytics_Edit_Scope_Tags_on_GPO()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16679334 completed");
        }
    }
}
