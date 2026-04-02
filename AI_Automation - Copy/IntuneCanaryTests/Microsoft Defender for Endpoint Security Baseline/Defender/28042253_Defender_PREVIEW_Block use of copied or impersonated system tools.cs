using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042253_Defender_PREVIEW_Block_Use_Of_Copied_Or_Impersonated_System_Tools : SecurityBaseline
    {
        [Test]
        public async Task Test_28042253_Defender_PREVIEW_Block_Use_Of_Copied_Or_Impersonated_System_Tools()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042253 completed");
        }
    }
}
