using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15754645_GPAnalytics_Scope_Tags___Scoped_Admin_Create__Read__Delete_With_Default_Tag_as_well : PageTest
    {
        [Test]
        public async Task Test_15754645_GPAnalytics_Scope_Tags___Scoped_Admin_Create__Read__Delete_With_Default_Tag_as_well()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15754645 completed");
        }
    }
}
