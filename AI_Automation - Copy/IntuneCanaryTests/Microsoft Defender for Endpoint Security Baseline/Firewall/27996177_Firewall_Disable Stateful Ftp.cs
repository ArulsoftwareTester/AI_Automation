using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T27996177_Firewall_Disable_Stateful_Ftp : SecurityBaseline
    {
        [Test]
        public async Task Test_27996177_Firewall_Disable_Stateful_Ftp()
        {
            await IPLogin(Page);
            await createProfile_Win365(Page, "Microsoft Defender for Endpoint Security Baseline");
            await MDMPolicySync(Page);
            Console.WriteLine("Test_27996177 completed");
        }
    }
}
