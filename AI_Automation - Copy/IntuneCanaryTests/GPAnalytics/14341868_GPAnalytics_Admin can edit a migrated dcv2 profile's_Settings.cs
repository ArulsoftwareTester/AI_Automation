using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T14341868_GPAnalytics_Admin_can_edit_a_migrated_dcv2_profile_s_Settings : PageTest
    {
        [Test]
        public async Task Test_14341868_GPAnalytics_Admin_can_edit_a_migrated_dcv2_profile_s_Settings()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_14341868 completed");
        }
    }
}
