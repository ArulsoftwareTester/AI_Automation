using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28068661_Administrative_Templates_Operating_System_Drives_Configure_TPM_startup_key : SecurityBaseline
    {
        [Test]
        public async Task Test_28068661_Administrative_Templates_Operating_System_Drives_Configure_TPM_startup_key()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28068661 completed");
        }
    }
}
