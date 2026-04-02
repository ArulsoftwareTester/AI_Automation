using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8907032_DC_Endpoint_Protection_WindowsEncryption_Minimum_PIN_Length
    {
        [Test]
        public async Task Test_8907032_DC_Endpoint_Protection_WindowsEncryption_Minimum_PIN_Length()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8907032 completed");
        }
    }
}
