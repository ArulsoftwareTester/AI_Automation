using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042479_Defender_Submit_Samples_Consent : SecurityBaseline
    {
        [Test]
        public async Task Test_28042479_Defender_Submit_Samples_Consent()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28042479 completed");
        }
    }
}
