using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042353_Defender_Block_Office_Applications_From_Injecting_Code_Into_Other_Processes : SecurityBaseline
    {
        [Test]
        public async Task Test_28042353_Defender_Block_Office_Applications_From_Injecting_Code_Into_Other_Processes()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042353 completed");
        }
    }
}
