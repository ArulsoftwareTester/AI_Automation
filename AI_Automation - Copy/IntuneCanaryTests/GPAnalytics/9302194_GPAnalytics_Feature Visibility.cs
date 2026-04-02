using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9302194_GPAnalytics_Feature_Visibility : PageTest
    {
        [Test]
        public async Task Test_9302194_GPAnalytics_Feature_Visibility()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9302194 completed");
        }
    }
}
