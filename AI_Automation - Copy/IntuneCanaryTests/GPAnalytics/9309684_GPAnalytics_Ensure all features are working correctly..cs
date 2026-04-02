using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9309684_GPAnalytics_Ensure_all_features_are_working_correctly_ : PageTest
    {
        [Test]
        public async Task Test_9309684_GPAnalytics_Ensure_all_features_are_working_correctly_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9309684 completed");
        }
    }
}
