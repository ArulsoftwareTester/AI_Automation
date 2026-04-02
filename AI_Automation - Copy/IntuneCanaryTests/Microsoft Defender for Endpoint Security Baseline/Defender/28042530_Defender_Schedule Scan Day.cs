using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042530_Defender_Schedule_Scan_Day : SecurityBaseline
    {
        [Test]
        public async Task Test_28042530_Defender_Schedule_Scan_Day()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042530 completed");
        }
    }
}
