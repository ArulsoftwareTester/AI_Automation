using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8914918_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_IT_contact_information
    {
        [Test]
        public async Task Test_8914918_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_IT_contact_information()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8914918 completed");
        }
    }
}
