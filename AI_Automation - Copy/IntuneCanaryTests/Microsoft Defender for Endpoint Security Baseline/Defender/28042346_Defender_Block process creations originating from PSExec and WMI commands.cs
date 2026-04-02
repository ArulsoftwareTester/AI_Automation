using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042346_Defender_Block_Process_Creations_Originating_From_PSExec_And_WMI_Commands : SecurityBaseline
    {
        [Test]
        public async Task Test_28042346_Defender_Block_Process_Creations_Originating_From_PSExec_And_WMI_Commands()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042346 completed");
        }
    }
}
