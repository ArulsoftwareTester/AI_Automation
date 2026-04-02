using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27996258_Firewall_Security_association_idle_time : SecurityBaseline
    {
        [Test]
        public async Task Test_27996258_Firewall_Security_association_idle_time()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_27996258 completed");
        }
    }
}
