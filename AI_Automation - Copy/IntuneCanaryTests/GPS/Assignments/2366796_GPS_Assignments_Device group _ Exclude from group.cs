using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2366796_GPS_Assignments_Device_group___Exclude_from_group : PageTest
    {
        [Test]
        public async Task Test_2366796_GPS_Assignments_Device_group___Exclude_from_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2366796 completed");
        }
    }
}
