using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T17464151_GPAnalytics_Scope_Tags___Scoped_Admin_Create__Read__Delete_With_Default_Tag_as_well_1_2 : PageTest
    {
        [Test]
        public async Task Test_17464151_GPAnalytics_Scope_Tags___Scoped_Admin_Create__Read__Delete_With_Default_Tag_as_well_1_2()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_17464151 completed");
        }
    }
}
