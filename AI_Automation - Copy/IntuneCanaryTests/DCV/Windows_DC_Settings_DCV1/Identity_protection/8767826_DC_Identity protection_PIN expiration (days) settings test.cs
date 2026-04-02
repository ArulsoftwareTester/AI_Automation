using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767826_DC_Identity_protection_PIN_expiration_days_settings_test
    {
        [Test]
        public async Task Test_8767826_DC_Identity_protection_PIN_expiration_days_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767826 completed");
        }
    }
}
