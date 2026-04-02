using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28066555_Administrative_Templates_Operating_System_Drives_Allow_devices_compliant_with_InstantGo_or_HSTI_to_opt_out_of_pre_boot_PIN : SecurityBaseline
    {
        [Test]
        public async Task Test_28066555_Administrative_Templates_Operating_System_Drives_Allow_devices_compliant_with_InstantGo_or_HSTI_to_opt_out_of_pre_boot_PIN()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28066555 completed");
        }
    }
}
