using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T28042068_Defender_Block_Office_communication_application_from_creating_child_processes
    {
        [Test]
        public async Task Test_28042068_Defender_Block_Office_communication_application_from_creating_child_processes()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            await securityBaseline.createProfile_Win365(null!, "Microsoft Defender for Endpoint Security Baseline");
            await securityBaseline.MDMPolicySync(null!);
            Console.WriteLine("Test_28042068 completed");
        }
    }
}
