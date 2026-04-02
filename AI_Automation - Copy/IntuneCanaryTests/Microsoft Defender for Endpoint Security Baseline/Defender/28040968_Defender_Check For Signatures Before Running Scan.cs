using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28040968_Defender_Check_For_Signatures_Before_Running_Scan : SecurityBaseline
    {
        [Test]
        public async Task Test_28040968_Defender_Check_For_Signatures_Before_Running_Scan()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28040968 completed");
        }
    }
}
