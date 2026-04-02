using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651280_Enrollment_UAC_IWMustReEnrollTheDeviceAfterItHasBeenPreviouslyRemoved : PageTest
    {
        [Test]
        public async Task Test_651280_Enrollment_UAC_IWMustReEnrollTheDeviceAfterItHasBeenPreviouslyRemoved()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for verifying IW must re-enroll device after it has been removed
            Console.WriteLine("Test_651280: Enrollment - UAC IW must re enroll the device after it has been previously Removed");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651280 completed");
        }
    }
}
