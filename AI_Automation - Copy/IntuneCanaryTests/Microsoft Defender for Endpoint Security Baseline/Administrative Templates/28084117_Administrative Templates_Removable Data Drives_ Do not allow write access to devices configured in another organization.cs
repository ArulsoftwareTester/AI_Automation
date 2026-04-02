using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28084117_Administrative_Templates_Removable_Data_Drives_Do_not_allow_write_access_to_devices_configured_in_another_organization : SecurityBaseline
    {
        [Test]
        public async Task Test_28084117_Administrative_Templates_Removable_Data_Drives_Do_not_allow_write_access_to_devices_configured_in_another_organization()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28084117 completed");
        }
    }
}
