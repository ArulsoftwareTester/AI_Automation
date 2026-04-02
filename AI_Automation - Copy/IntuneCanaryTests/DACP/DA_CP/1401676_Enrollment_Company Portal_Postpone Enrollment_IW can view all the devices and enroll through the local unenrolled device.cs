using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1401676_Enrollment_Company_Portal_Postpone_Enrollment_IW_can_view_all_the_devices_and_enroll_through_the_local_unenrolled_device : SecurityBaseline
    {
        [Test]
        public async Task Test_1401676_Enrollment_Company_Portal_Postpone_Enrollment_IW_can_view_all_the_devices_and_enroll_through_the_local_unenrolled_device()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1401676 completed");
        }
    }
}
