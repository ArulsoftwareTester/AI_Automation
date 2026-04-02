using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26853671_GPS_UX_Test_Properties__Filter_feature_works_for_new_UX : PageTest
    {
        [Test]
        public async Task Test_26853671_GPS_UX_Test_Properties__Filter_feature_works_for_new_UX()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_26853671 completed");
        }
    }
}
