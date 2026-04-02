using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16679357_GPAnalytics__Scoped_Admin_Read_after_Edit_GPO_1_2 : PageTest
    {
        [Test]
        public async Task Test_16679357_GPAnalytics__Scoped_Admin_Read_after_Edit_GPO_1_2()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16679357 completed");
        }
    }
}
