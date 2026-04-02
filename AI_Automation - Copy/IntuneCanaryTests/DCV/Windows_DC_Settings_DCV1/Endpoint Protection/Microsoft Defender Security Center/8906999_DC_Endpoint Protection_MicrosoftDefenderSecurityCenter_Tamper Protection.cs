using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906999_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Tamper_Protection
    {
        [Test]
        public async Task Test_8906999_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Tamper_Protection()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906999 completed");
        }
    }
}
