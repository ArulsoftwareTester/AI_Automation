using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2287279_GPS_Assignments_User_group___Exclude_from_group : PageTest
    {
        [Test]
        public async Task Test_2287279_GPS_Assignments_User_group___Exclude_from_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2287279 completed");
        }
    }
}
