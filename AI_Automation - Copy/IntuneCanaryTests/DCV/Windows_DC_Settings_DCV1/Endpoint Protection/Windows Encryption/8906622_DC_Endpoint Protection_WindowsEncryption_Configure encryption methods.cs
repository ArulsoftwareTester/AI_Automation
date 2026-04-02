using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906622_DC_Endpoint_Protection_WindowsEncryption_Configure_encryption_methods
    {
        [Test]
        public async Task Test_8906622_DC_Endpoint_Protection_WindowsEncryption_Configure_encryption_methods()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906622 completed");
        }
    }
}
