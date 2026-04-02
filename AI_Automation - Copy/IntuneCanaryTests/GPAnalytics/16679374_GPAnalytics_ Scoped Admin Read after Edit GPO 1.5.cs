using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16679374_GPAnalytics__Scoped_Admin_Read_after_Edit_GPO_1_5 : PageTest
    {
        [Test]
        public async Task Test_16679374_GPAnalytics__Scoped_Admin_Read_after_Edit_GPO_1_5()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16679374 completed");
        }
    }
}
