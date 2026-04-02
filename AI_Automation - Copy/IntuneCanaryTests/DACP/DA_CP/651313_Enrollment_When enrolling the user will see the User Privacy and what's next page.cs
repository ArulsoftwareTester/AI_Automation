using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651313_Enrollment_When_enrolling_the_user_will_see_the_User_Privacy_and_whats_next_page : SecurityBaseline
    {
        [Test]
        public async Task Test_651313_Enrollment_When_enrolling_the_user_will_see_the_User_Privacy_and_whats_next_page()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_651313 completed");
        }
    }
}
