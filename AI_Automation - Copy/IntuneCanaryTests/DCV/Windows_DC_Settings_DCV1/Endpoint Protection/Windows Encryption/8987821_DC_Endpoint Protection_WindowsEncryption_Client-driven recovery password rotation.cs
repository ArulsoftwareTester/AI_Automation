using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8987821_DC_Endpoint_Protection_WindowsEncryption_Client_driven_recovery_password_rotation
    {
        [Test]
        public async Task Test_8987821_DC_Endpoint_Protection_WindowsEncryption_Client_driven_recovery_password_rotation()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8987821 completed");
        }
    }
}
