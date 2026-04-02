using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042048_Defender_Block_Win32_API_calls_from_Office_macros : SecurityBaseline
    {
        [Test]
        public async Task Test_28042048_Defender_Block_Win32_API_calls_from_Office_macros()
        {
            // Call helpers from securityBaseline.cs using inherited Page property
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);

            Console.WriteLine("Test_28042048 completed");
        }
    }
}
