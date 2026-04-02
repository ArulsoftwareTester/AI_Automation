using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27999152_Administrative_Templates_BitLocker_Drive_Encryption_Fixed_Data_Drives_Deny_write_access_to_fixed_drives_not_protected_by_BitLocke : PageTest
    {
        [Test]
        public async Task Test_27999152_Administrative_Templates_BitLocker_Drive_Encryption_Fixed_Data_Drives_Deny_write_access_to_fixed_drives_not_protected_by_BitLocke()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            await securityBaseline.createProfile_Win365(Page, "Security Baseline for Windows 10 and later");
            await securityBaseline.MDMPolicySync(Page);
            Console.WriteLine("Test_27999152 completed");
        }
    }
}
