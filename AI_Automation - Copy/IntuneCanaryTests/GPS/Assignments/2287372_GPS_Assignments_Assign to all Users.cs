using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2287372_GPS_Assignments_Assign_to_all_Users : PageTest
    {
        [Test]
        public async Task Test_2287372_GPS_Assignments_Assign_to_all_Users()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2287372 completed");
        }
    }
}
