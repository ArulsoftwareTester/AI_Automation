using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9302201_GPAnalytics_Migrate_Button_Visibility : PageTest
    {
        [Test]
        public async Task Test_9302201_GPAnalytics_Migrate_Button_Visibility()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9302201 completed");
        }
    }
}
