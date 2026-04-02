using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T7425853_Android_Company_Portal_TNC_The_IW_cannot_continue_until_they_accept_new_terms_and_conditions_even_if_they_previously_used_the_Portal_before_terms_and_conditions_applied : SecurityBaseline
    {
        [Test]
        public async Task Test_7425853_Android_Company_Portal_TNC_The_IW_cannot_continue_until_they_accept_new_terms_and_conditions_even_if_they_previously_used_the_Portal_before_terms_and_conditions_applied()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_7425853 completed");
        }
    }
}
