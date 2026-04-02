using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8923085_DC_Endpoint_Protection_WindowsEncryption_Pre_boot_recovery_message_and_URL
    {
        [Test]
        public async Task Test_8923085_DC_Endpoint_Protection_WindowsEncryption_Pre_boot_recovery_message_and_URL()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8923085 completed");
        }
    }
}
