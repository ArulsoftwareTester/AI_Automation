using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767839_DC_Identity_protection_Remember_PIN_history_settings_test
    {
        [Test]
        public async Task Test_8767839_DC_Identity_protection_Remember_PIN_history_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767839 completed");
        }
    }
}
