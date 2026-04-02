using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906863_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_TPM_firmware_update_warning
    {
        [Test]
        public async Task Test_8906863_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_TPM_firmware_update_warning()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906863 completed");
        }
    }
}
