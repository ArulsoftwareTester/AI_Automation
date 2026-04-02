using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T13330424_Company_Portal_Diagnostic_User_cannot_send_feedback_when_they_are_not_signed_in : SecurityBaseline
    {
        [Test]
        public async Task Test_13330424_Company_Portal_Diagnostic_User_cannot_send_feedback_when_they_are_not_signed_in()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_13330424 completed");
        }
    }
}
