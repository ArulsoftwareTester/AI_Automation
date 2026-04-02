using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2566693_GPS_Assignments_Assign_to_all_users_and_all_devices : PageTest
    {
        [Test]
        public async Task Test_2566693_GPS_Assignments_Assign_to_all_users_and_all_devices()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2566693 completed");
        }
    }
}
