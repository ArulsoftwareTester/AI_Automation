using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8905856_DC_Endpoint_Protection_WindowsEncryption_Encrypt_devices
    {
        [Test]
        public async Task Test_8905856_DC_Endpoint_Protection_WindowsEncryption_Encrypt_devices()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8905856 completed");
        }
    }
}
