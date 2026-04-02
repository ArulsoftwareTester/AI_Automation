using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28036855_Administrative_Templates_Select_the_encryption_method_for_operating_system_drives : SecurityBaseline
    {
        [Test]
        public async Task Test_28036855_Administrative_Templates_Select_the_encryption_method_for_operating_system_drives()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28036855 completed");
        }
    }
}
