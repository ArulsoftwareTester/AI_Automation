using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042635_Defender_Schedule_Quick_Scan_Time : SecurityBaseline
    {
        [Test]
        public async Task Test_28042635_Defender_Schedule_Quick_Scan_Time()
        {
            // Call helpers from securityBaseline.cs using inherited Page property
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);

            Console.WriteLine("Test_28042635 completed");
        }
    }
}
