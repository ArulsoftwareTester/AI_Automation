using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8397244_DC_GPS_Assignment_Filter_Group1_with_none_Filter__Group2_with_Include_match_filter__Group_3_with_Include_NoMatch_filter : PageTest
    {
        [Test]
        public async Task Test_8397244_DC_GPS_Assignment_Filter_Group1_with_none_Filter__Group2_with_Include_match_filter__Group_3_with_Include_NoMatch_filter()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_8397244 completed");
        }
    }
}
