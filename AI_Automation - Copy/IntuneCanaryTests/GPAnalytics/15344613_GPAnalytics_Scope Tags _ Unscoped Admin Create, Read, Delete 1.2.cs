using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15344613_GPAnalytics_Scope_Tags___Unscoped_Admin_Create__Read__Delete_1_2 : PageTest
    {
        [Test]
        public async Task Test_15344613_GPAnalytics_Scope_Tags___Unscoped_Admin_Create__Read__Delete_1_2()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15344613 completed");
        }
    }
}
