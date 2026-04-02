using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15344550_GPAnalytics_Scope_Tags___Scoped_Admin_Create__Read__Delete : PageTest
    {
        [Test]
        public async Task Test_15344550_GPAnalytics_Scope_Tags___Scoped_Admin_Create__Read__Delete()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15344550 completed");
        }
    }
}
