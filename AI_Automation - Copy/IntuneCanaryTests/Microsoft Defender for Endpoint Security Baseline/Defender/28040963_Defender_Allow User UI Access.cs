using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28040963_Defender_Allow_User_UI_Access : SecurityBaseline
    {
        [Test]
        public async Task Test_28040963_Defender_Allow_User_UI_Access()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28040963 completed");
        }
    }
}
