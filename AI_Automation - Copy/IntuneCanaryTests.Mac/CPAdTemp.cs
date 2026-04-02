using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class CPAdTemp : SecurityBaseline
    {
        [OneTimeSetUp]
        public void Setup()
        {
            // Force Playwright to launch browser in headed mode
            Environment.SetEnvironmentVariable("HEADED", "1");
            Console.WriteLine("OneTimeSetUp: Set HEADED=1 for visible browser execution");
        }

        [Test]
        public async Task Test_CreateProfileAdminTemplate()
        {
            Console.WriteLine("Test_CreateProfileAdminTemplate started...");
            Console.WriteLine("Browser should be visible with all URL navigation and interactions...");
            
            // Step 1: Login to Intune Portal
            Console.WriteLine("Step 1: Logging into Intune Portal");
            await IPLogin(Page);
            Console.WriteLine("Successfully logged into Intune Portal");
            
            // Step 2: Execute createProfileAdminTemplate
            Console.WriteLine("Step 2: Executing createProfileAdminTemplate");
            await createProfileAdminTemplate(Page, "Windows 10", "Prompt");
            Console.WriteLine("Successfully completed createProfileAdminTemplate");
            
            // Step 3: Execute VMSync
            Console.WriteLine("Step 3: Executing VMSync");
            await VMSync(Page);
            Console.WriteLine("Successfully completed VMSync");
            
            Console.WriteLine("Test completed successfully!");
        }
    }
}
