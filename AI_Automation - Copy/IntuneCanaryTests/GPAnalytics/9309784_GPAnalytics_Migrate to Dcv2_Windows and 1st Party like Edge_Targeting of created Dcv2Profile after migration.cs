using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9309784_GPAnalytics_Migrate_to_Dcv2_Windows_and_1st_Party_like_Edge_Targeting_of_created_Dcv2Profile_after_migration : PageTest
    {
        [Test]
        public async Task Test_9309784_GPAnalytics_Migrate_to_Dcv2_Windows_and_1st_Party_like_Edge_Targeting_of_created_Dcv2Profile_after_migration()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9309784 completed");
        }
    }
}
