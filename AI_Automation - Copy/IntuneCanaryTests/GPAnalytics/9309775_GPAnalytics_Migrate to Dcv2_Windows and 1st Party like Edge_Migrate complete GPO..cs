using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9309775_GPAnalytics_Migrate_to_Dcv2_Windows_and_1st_Party_like_Edge_Migrate_complete_GPO_ : PageTest
    {
        [Test]
        public async Task Test_9309775_GPAnalytics_Migrate_to_Dcv2_Windows_and_1st_Party_like_Edge_Migrate_complete_GPO_()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9309775 completed");
        }
    }
}
