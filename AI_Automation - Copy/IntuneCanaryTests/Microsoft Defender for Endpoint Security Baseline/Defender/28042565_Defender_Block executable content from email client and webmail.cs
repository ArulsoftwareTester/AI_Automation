using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042565_Defender_Block_Executable_Content_From_Email_Client_And_Webmail : SecurityBaseline
    {
        [Test]
        public async Task Test_28042565_Defender_Block_Executable_Content_From_Email_Client_And_Webmail()
        {
            // Call helpers from securityBaseline.cs using inherited Page property
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);

            Console.WriteLine("Test_28042565 completed");
        }
    }
}
