using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906755_DC_Endpoint_Protection_WindowsEncryption_Additional_authentication_at_startup_Require
    {
        [Test]
        public async Task Test_8906755_DC_Endpoint_Protection_WindowsEncryption_Additional_authentication_at_startup_Require()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906755 completed");
        }
    }
}
