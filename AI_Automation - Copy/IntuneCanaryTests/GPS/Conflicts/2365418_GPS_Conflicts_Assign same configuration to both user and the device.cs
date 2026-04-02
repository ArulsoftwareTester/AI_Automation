using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2365418_GPS_Conflicts_Assign_same_configuration_to_both_user_and_the_device : PageTest
    {
        [Test]
        public async Task Test_2365418_GPS_Conflicts_Assign_same_configuration_to_both_user_and_the_device()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2365418 completed");
        }
    }
}
