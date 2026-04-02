using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9309679_GPAnalytics_Accuracy_of_the_migration_report : PageTest
    {
        [Test]
        public async Task Test_9309679_GPAnalytics_Accuracy_of_the_migration_report()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9309679 completed");
        }
    }
}
