using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28037591_Defender_Allow_Archive_Scanning : SecurityBaseline
    {
        [Test]
        public async Task Test_28037591_Defender_Allow_Archive_Scanning()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28037591 completed");
        }
    }
}
