using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8915040_DC_Endpoint_Protection_WindowsEncryption_Save_BitLocker_recovery_information_to_Azure_Active_Directory
    {
        [Test]
        public async Task Test_8915040_DC_Endpoint_Protection_WindowsEncryption_Save_BitLocker_recovery_information_to_Azure_Active_Directory()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8915040 completed");
        }
    }
}
