using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T15344718Instructions_for_Assigning_scope_tags_to_a_user : PageTest
    {
        [Test]
        public async Task Test_15344718Instructions_for_Assigning_scope_tags_to_a_user()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_15344718Instructions completed");
        }
    }
}
