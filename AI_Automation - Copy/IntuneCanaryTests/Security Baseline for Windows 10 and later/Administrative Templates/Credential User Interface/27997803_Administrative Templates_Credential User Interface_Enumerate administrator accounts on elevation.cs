using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27997803_Administrative_Templates_Credential_User_Interface_Enumerate_administrator_accounts_on_elevation : PageTest
    {
        [Test]
        public async Task Test_27997803_Administrative_Templates_Credential_User_Interface_Enumerate_administrator_accounts_on_elevation()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later");
            await securityBaseline.MDMPolicySync(Page);
            Console.WriteLine("Test_27997803 completed");
        }
    }
}
