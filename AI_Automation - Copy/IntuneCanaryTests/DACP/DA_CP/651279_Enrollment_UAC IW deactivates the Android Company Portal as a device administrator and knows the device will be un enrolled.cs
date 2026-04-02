using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651279_Enrollment_UAC_IWDeactivatesTheAndroidCompanyPortalAsADeviceAdministratorAndKnowsTheDeviceWillBeUnEnrolled : PageTest
    {
        [Test]
        public async Task Test_651279_Enrollment_UAC_IWDeactivatesTheAndroidCompanyPortalAsADeviceAdministratorAndKnowsTheDeviceWillBeUnEnrolled()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for verifying IW deactivates Android Company Portal as device administrator
            Console.WriteLine("Test_651279: Enrollment - UAC IW deactivates the Android Company Portal as a device administrator and knows the device will be un enrolled");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651279 completed");
        }
    }
}
