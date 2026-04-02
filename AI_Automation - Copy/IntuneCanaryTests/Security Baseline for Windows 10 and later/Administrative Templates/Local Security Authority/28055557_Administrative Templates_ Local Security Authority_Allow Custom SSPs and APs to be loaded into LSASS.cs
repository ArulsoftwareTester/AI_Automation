using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28055557_Administrative_Templates_Local_Security_Authority_Allow_Custom_SSPs_and_APs_to_be_loaded_into_LSASS : SecurityBaseline
    {
        [Test]
        public async Task Test_28055557_Administrative_Templates_Local_Security_Authority_Allow_Custom_SSPs_and_APs_to_be_loaded_into_LSASS()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Security Baseline for Windows 10 and later");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28055557 completed");
        }
    }
}
