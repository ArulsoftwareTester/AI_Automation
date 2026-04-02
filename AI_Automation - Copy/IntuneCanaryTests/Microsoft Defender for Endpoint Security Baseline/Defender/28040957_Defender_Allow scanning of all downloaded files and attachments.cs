using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28040957_Defender_Allow_Scanning_Of_All_Downloaded_Files_And_Attachments : SecurityBaseline
    {
        [Test]
        public async Task Test_28040957_Defender_Allow_Scanning_Of_All_Downloaded_Files_And_Attachments()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_28040957 completed");
        }
    }
}
