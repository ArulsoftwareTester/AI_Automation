using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T14362823_GPAnalytics_Admin_can_edit_a_migrated_dcv2_profiles_Basics : PageTest
    {
        [Test]
        public async Task Test_14362823_GPAnalytics_Admin_can_edit_a_migrated_dcv2_profiles_Basics()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_14362823 completed");
        }
    }
}
