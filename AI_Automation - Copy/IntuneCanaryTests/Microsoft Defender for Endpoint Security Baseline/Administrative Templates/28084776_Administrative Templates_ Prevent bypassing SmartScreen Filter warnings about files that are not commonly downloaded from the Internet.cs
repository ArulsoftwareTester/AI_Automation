using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28084776_Administrative_Templates_Prevent_bypassing_SmartScreen_Filter_warnings_about_files_that_are_not_commonly_downloaded_from_the_Internet : SecurityBaseline
    {
        [Test]
        public async Task Test_28084776_Administrative_Templates_Prevent_bypassing_SmartScreen_Filter_warnings_about_files_that_are_not_commonly_downloaded_from_the_Internet()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28084776 completed");
        }
    }
}
