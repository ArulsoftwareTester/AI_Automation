using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1494623_Enrollment_Company_Portal_Postpone_Enrollment_Available_with_or_without_enrollment_apps_can_be_installed_without_enrollment : SecurityBaseline
    {
        [Test]
        public async Task Test_1494623_Enrollment_Company_Portal_Postpone_Enrollment_Available_with_or_without_enrollment_apps_can_be_installed_without_enrollment()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1494623 completed");
        }
    }
}
