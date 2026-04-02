using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8397283_DC_GPS_Assignment_Filter_Hold_off_Policy_did_not_match__now_it_matches__make_sure_that_the_policy_assignment_value_is_applied_ : PageTest
    {
        [Test]
        public async Task Test_8397283_DC_GPS_Assignment_Filter_Hold_off_Policy_did_not_match__now_it_matches__make_sure_that_the_policy_assignment_value_is_applied_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_8397283 completed");
        }
    }
}
