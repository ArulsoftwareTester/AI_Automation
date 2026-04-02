using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2372191_GPS_Assignments_Assign_to_device : PageTest
    {
        [Test]
        public async Task Test_2372191_GPS_Assignments_Assign_to_device()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2372191 completed");
        }
    }
}
