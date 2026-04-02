using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28037400_Firewall_Enable_Private_Network_Firewall : SecurityBaseline
    {
        [Test]
        public async Task Test_28037400_Firewall_Enable_Private_Network_Firewall()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28037400 completed");
        }
    }
}
