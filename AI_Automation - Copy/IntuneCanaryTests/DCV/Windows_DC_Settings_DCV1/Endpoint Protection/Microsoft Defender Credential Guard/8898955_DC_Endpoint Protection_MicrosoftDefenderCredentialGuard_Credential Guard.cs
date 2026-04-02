using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898955_DC_Endpoint_Protection_MicrosoftDefenderCredentialGuard_Credential_Guard
    {
        [Test]
        public async Task Test_8898955_DC_Endpoint_Protection_MicrosoftDefenderCredentialGuard_Credential_Guard()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898955 completed");
        }
    }
}
