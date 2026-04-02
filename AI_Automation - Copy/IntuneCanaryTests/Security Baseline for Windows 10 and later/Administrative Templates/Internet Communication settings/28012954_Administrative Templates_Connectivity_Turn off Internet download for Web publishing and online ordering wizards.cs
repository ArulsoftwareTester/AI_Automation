using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28012954_Administrative_Templates_Connectivity_Turn_off_Internet_download_for_Web_publishing_and_online_ordering_wizards : PageTest
    {
        [Test]
        public async Task Test_28012954_Administrative_Templates_Connectivity_Turn_off_Internet_download_for_Web_publishing_and_online_ordering_wizards()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later");
            await securityBaseline.MDMPolicySync(Page);
            Console.WriteLine("Test_28012954 completed");
        }
    }
}
