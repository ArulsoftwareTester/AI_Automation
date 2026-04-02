using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28040954_Defender_Allow_Realtime_Monitoring : SecurityBaseline
    {
        [Test]
        public async Task Test_28040954_Defender_Allow_Realtime_Monitoring()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28040954 completed");
        }
    }
}
