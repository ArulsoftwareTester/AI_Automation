using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28037298_Administrative_Templates_Fixed_Data_Drives_Choose_how_BitLocker_protected_fixed_drives_can_be_recovered : SecurityBaseline
    {
        [Test]
        public async Task Test_28037298_Administrative_Templates_Fixed_Data_Drives_Choose_how_BitLocker_protected_fixed_drives_can_be_recovered()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28037298 completed");
        }
    }
}
