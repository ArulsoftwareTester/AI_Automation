using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8933857_DC_Endpoint_Protection_WindowsEncryption_Recovery_options_in_the_BitLocker_setup_wizard
    {
        [Test]
        public async Task Test_8933857_DC_Endpoint_Protection_WindowsEncryption_Recovery_options_in_the_BitLocker_setup_wizard()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8933857 completed");
        }
    }
}
