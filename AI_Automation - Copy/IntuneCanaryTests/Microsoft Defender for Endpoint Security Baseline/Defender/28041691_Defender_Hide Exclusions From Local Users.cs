using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28041691_Defender_Hide_Exclusions_From_Local_Users : SecurityBaseline
    {
        [Test]
        public async Task Test_28041691_Defender_Hide_Exclusions_From_Local_Users()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28041691 completed");
        }
    }
}
