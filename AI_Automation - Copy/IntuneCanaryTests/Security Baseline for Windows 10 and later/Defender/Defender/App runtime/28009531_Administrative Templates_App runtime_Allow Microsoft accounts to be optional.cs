using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28009531_Administrative_Templates_App_runtime_Allow_Microsoft_accounts_to_be_optional : PageTest
    {
        [Test]
        public async Task Test_28009531_Administrative_Templates_App_runtime_Allow_Microsoft_accounts_to_be_optional()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page, "test", "PE");
            await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later");
            await securityBaseline.MDMPolicySync(Page);
            Console.WriteLine("Test_28009531 completed");
        }
    }
}
