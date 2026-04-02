using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27995765_Microsoft_Edge_Configure_Microsoft_Defender_SmartScreen : SecurityBaseline
    {
        [Test]
        public async Task Test_27995765_Microsoft_Edge_Configure_Microsoft_Defender_SmartScreen()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_27995765 completed");
        }
    }
}
