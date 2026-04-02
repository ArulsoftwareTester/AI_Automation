using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28083753_Administrative_Templates_Removable_Data_Drives_Control_use_of_BitLocker_on_removable_drives : SecurityBaseline
    {
        [Test]
        public async Task Test_28083753_Administrative_Templates_Removable_Data_Drives_Control_use_of_BitLocker_on_removable_drives()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28083753 completed");
        }
    }
}
