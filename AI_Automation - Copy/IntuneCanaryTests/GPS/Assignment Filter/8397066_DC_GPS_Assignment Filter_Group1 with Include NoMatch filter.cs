using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8397066_DC_GPS_Assignment_Filter_Group1_with_Include_NoMatch_filter : PageTest
    {
        [Test]
        public async Task Test_8397066_DC_GPS_Assignment_Filter_Group1_with_Include_NoMatch_filter()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_8397066 completed");
        }
    }
}
