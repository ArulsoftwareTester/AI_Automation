using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28084556_Administrative_Templates_Prevent_managing_SmartScreen_Filter : SecurityBaseline
    {
        [Test]
        public async Task Test_28084556_Administrative_Templates_Prevent_managing_SmartScreen_Filter()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28084556 completed");
        }
    }
}
