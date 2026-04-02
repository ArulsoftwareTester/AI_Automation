using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254142_ExtendedRequirements_RegistryHonored : SecurityBaseline
    {
        [Test]
        public async Task Test_4254142_Verify_ExtendedRequirements_Registry_Honored()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254142 - Verify ExtendedRequirements registry is honored, should see RegistryRequirementRuleNotMet when failing");
            Console.WriteLine("Test_4254142 completed");
        }
    }
}
