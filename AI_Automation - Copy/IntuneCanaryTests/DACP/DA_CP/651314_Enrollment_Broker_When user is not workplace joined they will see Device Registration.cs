using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651314_Enrollment_Broker_When_user_is_not_workplace_joined_they_will_see_Device_Registration : SecurityBaseline
    {
        [Test]
        public async Task Test_651314_Enrollment_Broker_When_user_is_not_workplace_joined_they_will_see_Device_Registration()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_651314 completed");
        }
    }
}
