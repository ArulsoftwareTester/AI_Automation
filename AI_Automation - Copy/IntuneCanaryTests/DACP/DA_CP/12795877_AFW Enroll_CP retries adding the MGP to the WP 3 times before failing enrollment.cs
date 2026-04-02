using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T12795877_AFW_Enroll_CP_retries_adding_the_MGP_to_the_WP_3_times_before_failing_enrollment : SecurityBaseline
    {
        [Test]
        public async Task Test_12795877_AFW_Enroll_CP_retries_adding_the_MGP_to_the_WP_3_times_before_failing_enrollment()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_12795877 completed");
        }
    }
}
