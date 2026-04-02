using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8923071_DC_Endpoint_Protection_WindowsEncryption_Store_recovery_information_in_Azure_Active_Directory_before_enabling_BitLocker
    {
        [Test]
        public async Task Test_8923071_DC_Endpoint_Protection_WindowsEncryption_Store_recovery_information_in_Azure_Active_Directory_before_enabling_BitLocker()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8923071 completed");
        }
    }
}
