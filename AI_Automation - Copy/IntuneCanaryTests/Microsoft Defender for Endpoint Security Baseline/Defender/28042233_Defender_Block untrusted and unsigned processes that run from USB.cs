using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042233_Defender_Block_Untrusted_And_Unsigned_Processes_That_Run_From_USB : SecurityBaseline
    {
        [Test]
        public async Task Test_28042233_Defender_Block_Untrusted_And_Unsigned_Processes_That_Run_From_USB()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042233 completed");
        }
    }
}
