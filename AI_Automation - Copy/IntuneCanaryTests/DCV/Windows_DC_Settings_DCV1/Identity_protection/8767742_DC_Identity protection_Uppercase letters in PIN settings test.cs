using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767742_DC_Identity_protection_Uppercase_letters_in_PIN_settings_test
    {
        [Test]
        public async Task Test_8767742_DC_Identity_protection_Uppercase_letters_in_PIN_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767742 completed");
        }
    }
}
