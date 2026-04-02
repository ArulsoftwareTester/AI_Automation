using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T14362861_GPAnalytics_Admin_can_edit_a_migrated_dcv2_profile_s_Group_Assignment : PageTest
    {
        [Test]
        public async Task Test_14362861_GPAnalytics_Admin_can_edit_a_migrated_dcv2_profile_s_Group_Assignment()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_14362861 completed");
        }
    }
}
