using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28012889_Administrative_Templates_Group_Policy_Configure_registry_policy_processing : PageTest
    {
        [Test]
        public async Task Test_28012889_Administrative_Templates_Group_Policy_Configure_registry_policy_processing()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later");
            await securityBaseline.MDMPolicySync(Page);
            Console.WriteLine("Test_28012889 completed");
        }
    }
}
