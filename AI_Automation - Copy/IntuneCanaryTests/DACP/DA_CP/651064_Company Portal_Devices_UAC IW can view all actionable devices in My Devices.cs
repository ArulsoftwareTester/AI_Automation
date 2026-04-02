using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651064_CompanyPortal_Devices_UAC_IW_CanViewAllActionableDevicesInMyDevices : PageTest
    {
        [Test]
        public async Task Test_651064_CompanyPortal_Devices_UAC_IW_CanViewAllActionableDevicesInMyDevices()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for viewing all actionable devices in My Devices
            Console.WriteLine("Test_651064: Company Portal - Devices - UAC IW can view all actionable devices in My Devices");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651064 completed");
        }
    }
}
