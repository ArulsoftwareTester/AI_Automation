using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8934138_DC_Endpoint_Protection_WindowsEncryption_Save_BitLocker_recovery_information_to_Azure_Active_Directory
    {
        [Test]
        public async Task Test_8934138_DC_Endpoint_Protection_WindowsEncryption_Save_BitLocker_recovery_information_to_Azure_Active_Directory()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8934138 completed");
        }
    }
}
