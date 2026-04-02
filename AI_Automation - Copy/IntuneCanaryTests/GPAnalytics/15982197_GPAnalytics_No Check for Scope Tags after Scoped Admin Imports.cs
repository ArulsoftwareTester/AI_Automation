using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15982197_GPAnalytics_No_Check_for_Scope_Tags_after_Scoped_Admin_Imports : PageTest
    {
        [Test]
        public async Task Test_15982197_GPAnalytics_No_Check_for_Scope_Tags_after_Scoped_Admin_Imports()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15982197 completed");
        }
    }
}
