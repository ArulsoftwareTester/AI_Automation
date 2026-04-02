using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class _651401_Settings_Knox_Device_encryption_applies_NewSvcNewClient: PageTest
    {
        [Test]
        public async Task Test_651401_Settings_Knox_Device_encryption_applies_NewSvcNewClient()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            
            // Test implementation for Settings Knox Device encryption
            Console.WriteLine("Test_651401: Settings_Knox_Device encryption applies");
            
            // Add your test logic here
            
            Console.WriteLine("Test_651401 completed");
        }
    }
}
