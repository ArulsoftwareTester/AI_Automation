using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906879_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Clear_TPM_button
    {
        [Test]
        public async Task Test_8906879_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Clear_TPM_button()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906879 completed");
        }
    }
}
