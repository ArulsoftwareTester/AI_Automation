using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28084445_Administrative_Templates_Configure_Windows_Defender_SmartScreen : SecurityBaseline
    {
        [Test]
        public async Task Test_28084445_Administrative_Templates_Configure_Windows_Defender_SmartScreen()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28084445 completed");
        }
    }
}
