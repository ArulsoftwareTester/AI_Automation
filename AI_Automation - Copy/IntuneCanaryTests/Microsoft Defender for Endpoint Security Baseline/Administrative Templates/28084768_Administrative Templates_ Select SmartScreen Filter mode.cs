using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28084768_Administrative_Templates_Select_SmartScreen_Filter_mode : SecurityBaseline
    {
        [Test]
        public async Task Test_28084768_Administrative_Templates_Select_SmartScreen_Filter_mode()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28084768 completed");
        }
    }
}
