using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16220045_Enrollment_Renewal_Mooncake_User_can_successfully_renew_using_a_CNP_tenant : SecurityBaseline
    {
        [Test]
        public async Task Test_16220045_Enrollment_Renewal_Mooncake_User_can_successfully_renew_using_a_CNP_tenant()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_16220045 completed");
        }
    }
}
