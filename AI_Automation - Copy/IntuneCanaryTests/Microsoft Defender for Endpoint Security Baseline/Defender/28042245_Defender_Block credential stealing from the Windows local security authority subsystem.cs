using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042245_Defender_Block_Credential_Stealing_From_The_Windows_Local_Security_Authority_Subsystem : SecurityBaseline
    {
        [Test]
        public async Task Test_28042245_Defender_Block_Credential_Stealing_From_The_Windows_Local_Security_Authority_Subsystem()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042245 completed");
        }
    }
}
