using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28040952_Defender_Allow_On_Access_Protection : SecurityBaseline
    {
        [Test]
        public async Task Test_28040952_Defender_Allow_On_Access_Protection()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28040952 completed");
        }
    }
}
