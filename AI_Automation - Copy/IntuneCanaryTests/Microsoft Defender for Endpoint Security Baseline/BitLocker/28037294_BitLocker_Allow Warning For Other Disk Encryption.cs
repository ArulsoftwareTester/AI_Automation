using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28037294_BitLocker_Allow_Warning_For_Other_Disk_Encryption : SecurityBaseline
    {
        [Test]
        public async Task Test_28037294_BitLocker_Allow_Warning_For_Other_Disk_Encryption()
        {
            // Call helpers from securityBaseline.cs using inherited Page property
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);

            Console.WriteLine("Test_28037294 completed");
        }
    }
}
