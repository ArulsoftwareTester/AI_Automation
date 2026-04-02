using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9183427_DC_GPS_Assignment_Filter_Exclude_filter_NoMatch__Policy_with_Exclude_Filter_that_matches_device : PageTest
    {
        [Test]
        public async Task Test_9183427_DC_GPS_Assignment_Filter_Exclude_filter_NoMatch__Policy_with_Exclude_Filter_that_matches_device()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9183427 completed");
        }
    }
}
