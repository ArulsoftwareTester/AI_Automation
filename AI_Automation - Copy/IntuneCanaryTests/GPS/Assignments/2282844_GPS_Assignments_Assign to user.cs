using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2282844_GPS_Assignments_Assign_to_user : PageTest
    {
        [Test]
        public async Task Test_2282844_GPS_Assignments_Assign_to_user()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2282844 completed");
        }
    }
}
