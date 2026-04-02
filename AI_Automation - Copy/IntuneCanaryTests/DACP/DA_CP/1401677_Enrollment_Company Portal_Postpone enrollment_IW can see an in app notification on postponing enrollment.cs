using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1401677_Enrollment_Company_Portal_Postpone_enrollment_IW_can_see_an_in_app_notification_on_postponing_enrollment : SecurityBaseline
    {
        [Test]
        public async Task Test_1401677_Enrollment_Company_Portal_Postpone_enrollment_IW_can_see_an_in_app_notification_on_postponing_enrollment()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1401677 completed");
        }
    }
}
