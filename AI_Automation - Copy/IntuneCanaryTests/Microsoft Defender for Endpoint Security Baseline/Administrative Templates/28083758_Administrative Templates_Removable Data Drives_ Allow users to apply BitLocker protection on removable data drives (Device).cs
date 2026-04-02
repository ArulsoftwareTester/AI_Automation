using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28083758_Administrative_Templates_Removable_Data_Drives_Allow_users_to_apply_BitLocker_protection_on_removable_data_drives_Device : SecurityBaseline
    {
        [Test]
        public async Task Test_28083758_Administrative_Templates_Removable_Data_Drives_Allow_users_to_apply_BitLocker_protection_on_removable_data_drives_Device()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28083758 completed");
        }
    }
}
