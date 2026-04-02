using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28066594_Administrative_Templates_Operating_System_Drives_Choose_how_BitLocker_protected_operating_system_drives_can_be_recovered : SecurityBaseline
    {
        [Test]
        public async Task Test_28066594_Administrative_Templates_Operating_System_Drives_Choose_how_BitLocker_protected_operating_system_drives_can_be_recovered()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28066594 completed");
        }
    }
}
