using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042074_Defender_Block_All_Office_Applications_From_Creating_Child_Processes : SecurityBaseline
    {
        [Test]
        public async Task Test_28042074_Defender_Block_All_Office_Applications_From_Creating_Child_Processes()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042074 completed");
        }
    }
}
