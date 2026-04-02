using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767435_DC_Identity_protection_Minimum_PIN_length_settings_test
    {
        [Test]
        public async Task Test_8767435_DC_Identity_protection_Minimum_PIN_length_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767435 completed");
        }
    }
}
