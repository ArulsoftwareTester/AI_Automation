using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042238_Defender_Block_Adobe_Reader_From_Creating_Child_Processes : SecurityBaseline
    {
        [Test]
        public async Task Test_28042238_Defender_Block_Adobe_Reader_From_Creating_Child_Processes()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042238 completed");
        }
    }
}
