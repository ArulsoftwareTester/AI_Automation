using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651316_Enrollment_User_can_postpone_Setup_after_enrolling : SecurityBaseline
    {
        [Test]
        public async Task Test_651316_Enrollment_User_can_postpone_Setup_after_enrolling()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_651316 completed");
        }
    }
}
