using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28068376_Administrative_Templates_Operating_System_Drives_Enable_use_of_BitLocker_authentication_requiring_preboot_keyboard_input_on_slates : SecurityBaseline
    {
        [Test]
        public async Task Test_28068376_Administrative_Templates_Operating_System_Drives_Enable_use_of_BitLocker_authentication_requiring_preboot_keyboard_input_on_slates()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28068376 completed");
        }
    }
}
