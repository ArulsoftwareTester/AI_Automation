using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9183416_DC_GPS_Assignment_Filter_Exclude_filter_Match_Policy_with_Exclude_Filter_that_matches_device : PageTest
    {
        [Test]
        public async Task Test_9183416_DC_GPS_Assignment_Filter_Exclude_filter_Match_Policy_with_Exclude_Filter_that_matches_device()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9183416 completed");
        }
    }
}
