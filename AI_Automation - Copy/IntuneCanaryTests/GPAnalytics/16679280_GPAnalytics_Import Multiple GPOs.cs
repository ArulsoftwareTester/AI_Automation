using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16679280_GPAnalytics_Import_Multiple_GPOs : PageTest
    {
        [Test]
        public async Task Test_16679280_GPAnalytics_Import_Multiple_GPOs()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_16679280 completed");
        }
    }
}
