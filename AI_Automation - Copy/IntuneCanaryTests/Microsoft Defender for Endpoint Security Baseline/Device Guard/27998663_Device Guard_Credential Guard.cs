using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27998663_Device_Guard_Credential_Guard : SecurityBaseline
    {
        [Test]
        public async Task Test_27998663_Device_Guard_Credential_Guard()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);

            Console.WriteLine("Test_27998663 completed");
        }
    }
}
