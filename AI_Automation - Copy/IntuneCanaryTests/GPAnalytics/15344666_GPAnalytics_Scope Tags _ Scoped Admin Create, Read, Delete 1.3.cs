using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15344666_GPAnalytics_Scope_Tags___Scoped_Admin_Create__Read__Delete_1_3 : PageTest
    {
        [Test]
        public async Task Test_15344666_GPAnalytics_Scope_Tags___Scoped_Admin_Create__Read__Delete_1_3()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15344666 completed");
        }
    }
}
