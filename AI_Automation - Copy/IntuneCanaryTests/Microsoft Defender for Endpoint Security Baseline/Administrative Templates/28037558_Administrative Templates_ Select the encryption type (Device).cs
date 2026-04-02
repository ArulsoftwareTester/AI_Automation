using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28037558_Administrative_Templates_Select_the_encryption_type_Device : SecurityBaseline
    {
        [Test]
        public async Task Test_28037558_Administrative_Templates_Select_the_encryption_type_Device()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28037558 completed");
        }
    }
}
