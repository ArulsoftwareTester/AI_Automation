using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16679364_GPAnalytics__Scoped_Admin_Read_after_Edit_GPO_1_3 : PageTest
    {
        [Test]
        public async Task Test_16679364_GPAnalytics__Scoped_Admin_Read_after_Edit_GPO_1_3()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16679364 completed");
        }
    }
}
