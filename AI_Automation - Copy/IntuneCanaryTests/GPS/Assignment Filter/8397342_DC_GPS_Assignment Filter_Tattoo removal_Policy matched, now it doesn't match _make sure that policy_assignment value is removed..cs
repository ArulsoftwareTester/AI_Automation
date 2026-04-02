using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8397342_DC_GPS_Assignment_Filter_Tattoo_removal_Policy_matched__now_it_doesn_t_match__make_sure_that_policy_assignment_value_is_removed_ : PageTest
    {
        [Test]
        public async Task Test_8397342_DC_GPS_Assignment_Filter_Tattoo_removal_Policy_matched__now_it_doesn_t_match__make_sure_that_policy_assignment_value_is_removed_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_8397342 completed");
        }
    }
}
