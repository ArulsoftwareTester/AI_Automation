using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28008770_Administrative_Templates_BitLocker_Drive_Encryption_Removable_Data_Drives_Deny_write_access_to_removable_drives_not_protected_by_BitLocker : PageTest
    {
        [Test]
        public async Task Test_28008770_Administrative_Templates_BitLocker_Drive_Encryption_Removable_Data_Drives_Deny_write_access_to_removable_drives_not_protected_by_BitLocker()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later");
            await securityBaseline.MDMPolicySync(Page);
            Console.WriteLine("Test_28008770 completed");
        }
    }
}
