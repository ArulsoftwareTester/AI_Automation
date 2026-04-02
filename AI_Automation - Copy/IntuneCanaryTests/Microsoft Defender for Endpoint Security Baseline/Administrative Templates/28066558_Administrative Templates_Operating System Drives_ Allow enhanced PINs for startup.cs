using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28066558_Administrative_Templates_Operating_System_Drives_Allow_enhanced_PINs_for_startup : SecurityBaseline
    {
        [Test]
        public async Task Test_28066558_Administrative_Templates_Operating_System_Drives_Allow_enhanced_PINs_for_startup()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28066558 completed");
        }
    }
}
