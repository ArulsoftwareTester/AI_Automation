using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27996130_Firewall_Certificate_revocation_list_verification : SecurityBaseline
    {
        [Test]
        public async Task Test_27996130_Firewall_Certificate_revocation_list_verification()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_27996130 completed");
        }
    }
}
