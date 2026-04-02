using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767432_DC_Identity_protection_Use_security_keys_for_sign_in_settings_test
    {
        [Test]
        public async Task Test_8767432_DC_Identity_protection_Use_security_keys_for_sign_in_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767432 completed");
        }
    }
}
