using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16679353_GPAnalytics__Scoped_Admin_Read_After_Edit : PageTest
    {
        [Test]
        public async Task Test_16679353_GPAnalytics__Scoped_Admin_Read_After_Edit()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16679353 completed");
        }
    }
}
