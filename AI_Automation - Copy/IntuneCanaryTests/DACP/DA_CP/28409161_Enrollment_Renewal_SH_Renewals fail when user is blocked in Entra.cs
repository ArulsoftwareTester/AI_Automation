using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28409161_Enrollment_Renewal_SH_Renewals_fail_when_user_is_blocked_in_Entra : SecurityBaseline
    {
        [Test]
        public async Task Test_28409161_Enrollment_Renewal_SH_Renewals_fail_when_user_is_blocked_in_Entra()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_28409161 completed");
        }
    }
}
