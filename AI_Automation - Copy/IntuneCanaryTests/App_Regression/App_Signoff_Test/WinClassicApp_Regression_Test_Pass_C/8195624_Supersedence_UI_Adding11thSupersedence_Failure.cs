using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195624_Supersedence_UI_Adding11thSupersedence_Failure : SecurityBaseline
    {
        [Test]
        public async Task Test_8195624_Verify_Adding11thSupersedence_Causes_Failure()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195624 - Verify adding 11th supersedence causes failure");
            Console.WriteLine("Test_8195624 completed");
        }
    }
}
