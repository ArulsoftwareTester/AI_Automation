using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9309790_GPAnalytics_Migrate_Chrome_settings_in_GPO_to_Edge_Settings_in_Dcv2_3rd_Party_owner_Aasawari : PageTest
    {
        [Test]
        public async Task Test_9309790_GPAnalytics_Migrate_Chrome_settings_in_GPO_to_Edge_Settings_in_Dcv2_3rd_Party_owner_Aasawari()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9309790 completed");
        }
    }
}
