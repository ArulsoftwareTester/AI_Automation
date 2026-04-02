using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898925_DC_Endpoint_Protection_MicrosoftDefenderApplicationControl_Application_control_code_integrity_policies_Enforce
    {
        [Test]
        public async Task Test_8898925_DC_Endpoint_Protection_MicrosoftDefenderApplicationControl_Application_control_code_integrity_policies_Enforce()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898925 completed");
        }
    }
}
