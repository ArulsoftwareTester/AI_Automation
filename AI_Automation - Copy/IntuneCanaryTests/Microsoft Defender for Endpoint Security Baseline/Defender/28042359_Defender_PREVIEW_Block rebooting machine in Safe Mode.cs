using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042359_Defender_PREVIEW_Block_Rebooting_Machine_In_Safe_Mode : SecurityBaseline
    {
        [Test]
        public async Task Test_28042359_Defender_PREVIEW_Block_Rebooting_Machine_In_Safe_Mode()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042359 completed");
        }
    }
}
