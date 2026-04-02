using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T651099_CompanyPortal_UAC_IW_UserCanSeeAllInformationInTheirAADProfileThroughTheAndroidSSP : PageTest
    {
        [Test]
        public async Task Test_651099_CompanyPortal_UAC_IW_UserCanSeeAllInformationInTheirAADProfileThroughTheAndroidSSP()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for viewing all information in AAD profile through Android SSP
            Console.WriteLine("Test_651099: Company Portal - UAC IW user can see all information in their AAD profile through the Android SSP");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651099 completed");
        }
    }
}
