using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1401457_Enrollment_Company_Portal_Postpone_Enrollment_IW_can_view_all_apps_without_enrollment : SecurityBaseline
    {
        [Test]
        public async Task Test_1401457_Enrollment_Company_Portal_Postpone_Enrollment_IW_can_view_all_apps_without_enrollment()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1401457 completed");
        }
    }
}
