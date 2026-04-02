using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042349_Defender_Block_Office_Applications_From_Creating_Executable_Content : SecurityBaseline
    {
        [Test]
        public async Task Test_28042349_Defender_Block_Office_Applications_From_Creating_Executable_Content()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042349 completed");
        }
    }
}
