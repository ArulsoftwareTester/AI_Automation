using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27995992_Microsoft_Edge_Enable_new_SmartScreen_library : SecurityBaseline
    {
        [Test]
        public async Task Test_27995992_Microsoft_Edge_Enable_new_SmartScreen_library()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_27995992 completed");
        }
    }
}
