using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28036801_Administrative_Templates_Choose_drive_encryption_method_and_cipher_strength_Windows_10_Version_1511_and_later : SecurityBaseline
    {
        [Test]
        public async Task Test_28036801_Administrative_Templates_Choose_drive_encryption_method_and_cipher_strength_Windows_10_Version_1511_and_later()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28036801 completed");
        }
    }
}
