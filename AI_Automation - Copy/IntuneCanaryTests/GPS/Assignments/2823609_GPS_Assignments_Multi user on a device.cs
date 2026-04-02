using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2823609_GPS_Assignments_Multi_user_on_a_device : PageTest
    {
        [Test]
        public async Task Test_2823609_GPS_Assignments_Multi_user_on_a_device()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2823609 completed");
        }
    }
}
