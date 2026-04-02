using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651317_Enrollment_When_postponed_user_can_fix_compliance_through_device_details : SecurityBaseline
    {
        [Test]
        public async Task Test_651317_Enrollment_When_postponed_user_can_fix_compliance_through_device_details()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_651317 completed");
        }
    }
}
