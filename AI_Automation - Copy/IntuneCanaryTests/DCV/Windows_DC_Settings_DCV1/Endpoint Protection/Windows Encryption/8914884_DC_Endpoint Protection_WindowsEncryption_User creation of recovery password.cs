using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8914884_DC_Endpoint_Protection_WindowsEncryption_User_creation_of_recovery_password
    {
        [Test]
        public async Task Test_8914884_DC_Endpoint_Protection_WindowsEncryption_User_creation_of_recovery_password()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8914884 completed");
        }
    }
}
