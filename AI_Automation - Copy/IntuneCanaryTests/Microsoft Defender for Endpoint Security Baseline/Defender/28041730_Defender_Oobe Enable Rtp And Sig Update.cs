using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28041730_Defender_Oobe_Enable_Rtp_And_Sig_Update : SecurityBaseline
    {
        [Test]
        public async Task Test_28041730_Defender_Oobe_Enable_Rtp_And_Sig_Update()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28041730 completed");
        }
    }
}
