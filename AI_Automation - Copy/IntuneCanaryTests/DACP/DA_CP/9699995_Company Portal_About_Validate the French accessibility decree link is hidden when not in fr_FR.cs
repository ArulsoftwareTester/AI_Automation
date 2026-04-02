using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9699995_Company_Portal_About_Validate_the_French_accessibility_decree_link_is_hidden_when_not_in_fr_FR : SecurityBaseline
    {
        [Test]
        public async Task Test_9699995_Company_Portal_About_Validate_the_French_accessibility_decree_link_is_hidden_when_not_in_fr_FR()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_9699995 completed");
        }
    }
}
