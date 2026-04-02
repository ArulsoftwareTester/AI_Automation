using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906684_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Family_options
    {
        [Test]
        public async Task Test_8906684_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Family_options()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906684 completed");
        }
    }
}
