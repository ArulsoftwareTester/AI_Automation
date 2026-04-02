using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27995999_Microsoft_Edge_Prevent_bypassing_Microsoft_Defender_SmartScreen_prompts_for_sites : SecurityBaseline
    {
        [Test]
        public async Task Test_27995999_Microsoft_Edge_Prevent_bypassing_Microsoft_Defender_SmartScreen_prompts_for_sites()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_27995999 completed");
        }
    }
}
