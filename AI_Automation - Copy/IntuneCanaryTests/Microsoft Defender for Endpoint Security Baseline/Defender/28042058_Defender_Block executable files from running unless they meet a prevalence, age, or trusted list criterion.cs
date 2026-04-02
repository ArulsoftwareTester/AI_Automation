using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042058_Defender_Block_Executable_Files_From_Running_Unless_They_Meet_A_Prevalence_Age_Or_Trusted_List_Criterion : SecurityBaseline
    {
        [Test]
        public async Task Test_28042058_Defender_Block_Executable_Files_From_Running_Unless_They_Meet_A_Prevalence_Age_Or_Trusted_List_Criterion()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042058 completed");
        }
    }
}
