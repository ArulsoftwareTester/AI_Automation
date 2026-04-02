using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042466_Defender_Real_Time_Scan_Direction : SecurityBaseline
    {
        [Test]
        public async Task Test_28042466_Defender_Real_Time_Scan_Direction()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042466 completed");
        }
    }
}
