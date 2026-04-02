using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767867_DC_Identity_protection_Use_enhanced_anti_spoofing_when_available_settings_test
    {
        [Test]
        public async Task Test_8767867_DC_Identity_protection_Use_enhanced_anti_spoofing_when_available_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767867 completed");
        }
    }
}
