using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T16190039_Enrollment_Renewal_SH_User_can_successfully_renew_using_a_SH_tenant : SecurityBaseline
    {
        [Test]
        public async Task Test_16190039_Enrollment_Renewal_SH_User_can_successfully_renew_using_a_SH_tenant()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_16190039 completed");
        }
    }
}
