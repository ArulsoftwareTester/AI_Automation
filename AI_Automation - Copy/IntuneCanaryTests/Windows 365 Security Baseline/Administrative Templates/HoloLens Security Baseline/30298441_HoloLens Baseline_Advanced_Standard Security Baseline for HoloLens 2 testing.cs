using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T30298441_HoloLens_Baseline_Advanced_Standard_Security_Baseline_For_HoloLens_2_Testing
    {
        [Test]
        public async Task Test_30298441_HoloLens_Baseline_Advanced_Standard_Security_Baseline_For_HoloLens_2_Testing()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            await securityBaseline.createProfile_Win365(null!, "Windows 365");
            await securityBaseline.MDMPolicySync(null!);
            Console.WriteLine("Test_30298441 completed");
        }
    }
}
