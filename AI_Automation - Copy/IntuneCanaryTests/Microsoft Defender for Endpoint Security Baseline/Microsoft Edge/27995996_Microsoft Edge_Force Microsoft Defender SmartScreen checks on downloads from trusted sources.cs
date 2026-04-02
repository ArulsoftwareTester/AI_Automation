using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27995996_Microsoft_Edge_Force_Microsoft_Defender_SmartScreen_checks_on_downloads_from_trusted_sources : SecurityBaseline
    {
        [Test]
        public async Task Test_27995996_Microsoft_Edge_Force_Microsoft_Defender_SmartScreen_checks_on_downloads_from_trusted_sources()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_27995996 completed");
        }
    }
}
