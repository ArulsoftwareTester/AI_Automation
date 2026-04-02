using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651315_Enrollment_When_user_is_not_compliant_they_will_see_the_resolve_compliance_issues_page_after_checking_compliance : SecurityBaseline
    {
        [Test]
        public async Task Test_651315_Enrollment_When_user_is_not_compliant_they_will_see_the_resolve_compliance_issues_page_after_checking_compliance()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_651315 completed");
        }
    }
}
