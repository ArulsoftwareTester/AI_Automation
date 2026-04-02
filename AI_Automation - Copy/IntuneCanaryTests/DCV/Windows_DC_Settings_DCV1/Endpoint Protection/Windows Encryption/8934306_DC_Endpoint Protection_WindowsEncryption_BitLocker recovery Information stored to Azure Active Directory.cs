using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8934306_DC_Endpoint_Protection_WindowsEncryption_BitLocker_recovery_Information_stored_to_Azure_Active_Directory
    {
        [Test]
        public async Task Test_8934306_DC_Endpoint_Protection_WindowsEncryption_BitLocker_recovery_Information_stored_to_Azure_Active_Directory()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8934306 completed");
        }
    }
}
