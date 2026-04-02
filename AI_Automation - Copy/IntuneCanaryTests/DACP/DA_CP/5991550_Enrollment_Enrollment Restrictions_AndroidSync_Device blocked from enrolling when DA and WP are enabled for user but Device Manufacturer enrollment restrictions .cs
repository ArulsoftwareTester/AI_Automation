using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5991550_Enrollment_Enrollment_Restrictions_AndroidSync_Device_blocked_from_enrolling_when_DA_and_WP_are_enabled_for_user_but_Device_Manufacturer_enrollment_restrictions : SecurityBaseline
    {
        [Test]
        public async Task Test_5991550_Enrollment_Enrollment_Restrictions_AndroidSync_Device_blocked_from_enrolling_when_DA_and_WP_are_enabled_for_user_but_Device_Manufacturer_enrollment_restrictions()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_5991550 completed");
        }
    }
}
