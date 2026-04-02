using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042633_Defender_Cloud_Extended_Timeout : SecurityBaseline
    {
        [Test]
        public async Task Test_28042633_Defender_Cloud_Extended_Timeout()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042633 completed");
        }
    }
}
