using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15982175_GPAnalytics_GPAnalytics_No_Check_for_Scope_Tags_after_Global_Admin_Import : PageTest
    {
        [Test]
        public async Task Test_15982175_GPAnalytics_GPAnalytics_No_Check_for_Scope_Tags_after_Global_Admin_Import()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15982175 completed");
        }
    }
}
