using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8905814_DC_Endpoint_Protection_MicrosoftDefenderApplicationControl_Application_control_code_integrity_policies_Audit
    {
        [Test]
        public async Task Test_8905814_DC_Endpoint_Protection_MicrosoftDefenderApplicationControl_Application_control_code_integrity_policies_Audit()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8905814 completed");
        }
    }
}
