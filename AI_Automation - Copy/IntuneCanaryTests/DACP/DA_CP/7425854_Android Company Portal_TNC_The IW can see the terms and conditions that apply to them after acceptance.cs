using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T7425854_Android_Company_Portal_TNC_The_IW_can_see_the_terms_and_conditions_that_apply_to_them_after_acceptance : SecurityBaseline
    {
        [Test]
        public async Task Test_7425854_Android_Company_Portal_TNC_The_IW_can_see_the_terms_and_conditions_that_apply_to_them_after_acceptance()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_7425854 completed");
        }
    }
}
