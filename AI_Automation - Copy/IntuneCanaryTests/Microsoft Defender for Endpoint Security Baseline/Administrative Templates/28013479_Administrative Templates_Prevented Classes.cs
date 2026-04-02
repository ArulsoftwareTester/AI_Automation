using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28013479_Administrative_Templates_Prevented_Classes : SecurityBaseline
    {
        [Test]
        public async Task Test_28013479_Administrative_Templates_Prevented_Classes()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28013479 completed");
        }
    }
}
