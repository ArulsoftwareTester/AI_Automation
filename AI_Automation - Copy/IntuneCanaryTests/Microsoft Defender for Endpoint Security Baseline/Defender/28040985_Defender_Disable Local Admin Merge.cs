using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28040985_Defender_Disable_Local_Admin_Merge : SecurityBaseline
    {
        [Test]
        public async Task Test_28040985_Defender_Disable_Local_Admin_Merge()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28040985 completed");
        }
    }
}
