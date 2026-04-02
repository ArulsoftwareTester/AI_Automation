using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042360_Defender_Use_Advanced_Protection_Against_Ransomware : SecurityBaseline
    {
        [Test]
        public async Task Test_28042360_Defender_Use_Advanced_Protection_Against_Ransomware()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042360 completed");
        }
    }
}
