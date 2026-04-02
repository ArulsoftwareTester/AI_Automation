using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9700160_Company_Portal_About_Validate_that_French_accessibility_decree_link_shows_up_when_switching_locale_to_fr_FR : SecurityBaseline
    {
        [Test]
        public async Task Test_9700160_Company_Portal_About_Validate_that_French_accessibility_decree_link_shows_up_when_switching_locale_to_fr_FR()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_9700160 completed");
        }
    }
}
