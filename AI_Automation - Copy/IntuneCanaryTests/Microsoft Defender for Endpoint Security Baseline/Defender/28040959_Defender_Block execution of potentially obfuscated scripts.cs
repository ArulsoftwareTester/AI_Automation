using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28040959_Defender_Block_Execution_Of_Potentially_Obfuscated_Scripts : SecurityBaseline
    {
        [Test]
        public async Task Test_28040959_Defender_Block_Execution_Of_Potentially_Obfuscated_Scripts()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28040959 completed");
        }
    }
}
