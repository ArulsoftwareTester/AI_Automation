using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28013324_Administrative_Templates_Prevent_installation_of_devices_using_drivers_that_match_these_device_setup_classes : SecurityBaseline
    {
        [Test]
        public async Task Test_28013324_Administrative_Templates_Prevent_installation_of_devices_using_drivers_that_match_these_device_setup_classes()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28013324 completed");
        }
    }
}
