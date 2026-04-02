using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8396838_DC_GPS_Assignment_Filter_Group1_with_Include_Match_filter : PageTest
    {
        [Test]
        public async Task Test_8396838_DC_GPS_Assignment_Filter_Group1_with_Include_Match_filter()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_8396838 completed");
        }
    }
}
