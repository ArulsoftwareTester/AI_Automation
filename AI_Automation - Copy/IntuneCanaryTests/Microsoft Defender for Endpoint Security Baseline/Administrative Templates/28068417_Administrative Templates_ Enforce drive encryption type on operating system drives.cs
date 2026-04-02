using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28068417_Administrative_Templates_Enforce_drive_encryption_type_on_operating_system_drives : SecurityBaseline
    {
        [Test]
        public async Task Test_28068417_Administrative_Templates_Enforce_drive_encryption_type_on_operating_system_drives()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28068417 completed");
        }
    }
}
