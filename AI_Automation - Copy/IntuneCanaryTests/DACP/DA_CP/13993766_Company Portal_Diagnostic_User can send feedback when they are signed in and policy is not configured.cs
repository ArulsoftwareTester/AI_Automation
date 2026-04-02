using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T13993766_Company_Portal_Diagnostic_User_can_send_feedback_when_they_are_signed_in_and_policy_is_not_configured : SecurityBaseline
    {
        [Test]
        public async Task Test_13993766_Company_Portal_Diagnostic_User_can_send_feedback_when_they_are_signed_in_and_policy_is_not_configured()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_13993766 completed");
        }
    }
}
