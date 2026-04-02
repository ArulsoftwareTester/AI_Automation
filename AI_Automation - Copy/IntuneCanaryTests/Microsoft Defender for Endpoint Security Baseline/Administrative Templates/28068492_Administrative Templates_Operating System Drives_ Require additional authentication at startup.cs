using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28068492_Administrative_Templates_Operating_System_Drives_Require_additional_authentication_at_startup : SecurityBaseline
    {
        [Test]
        public async Task Test_28068492_Administrative_Templates_Operating_System_Drives_Require_additional_authentication_at_startup()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28068492 completed");
        }
    }
}
