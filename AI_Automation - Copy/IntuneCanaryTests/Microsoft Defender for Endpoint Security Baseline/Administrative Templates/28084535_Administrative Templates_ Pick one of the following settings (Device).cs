using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28084535_Administrative_Templates_Pick_one_of_the_following_settings_Device : SecurityBaseline
    {
        [Test]
        public async Task Test_28084535_Administrative_Templates_Pick_one_of_the_following_settings_Device()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28084535 completed");
        }
    }
}
