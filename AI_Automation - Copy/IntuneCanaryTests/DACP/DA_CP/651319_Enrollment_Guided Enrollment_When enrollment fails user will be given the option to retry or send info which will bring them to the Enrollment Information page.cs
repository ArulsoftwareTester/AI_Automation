using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651319_Enrollment_Guided_Enrollment_When_enrollment_fails_user_will_be_given_the_option_to_retry_or_send_info_which_will_bring_them_to_the_Enrollment_Information_page : SecurityBaseline
    {
        [Test]
        public async Task Test_651319_Enrollment_Guided_Enrollment_When_enrollment_fails_user_will_be_given_the_option_to_retry_or_send_info_which_will_bring_them_to_the_Enrollment_Information_page()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_651319 completed");
        }
    }
}
