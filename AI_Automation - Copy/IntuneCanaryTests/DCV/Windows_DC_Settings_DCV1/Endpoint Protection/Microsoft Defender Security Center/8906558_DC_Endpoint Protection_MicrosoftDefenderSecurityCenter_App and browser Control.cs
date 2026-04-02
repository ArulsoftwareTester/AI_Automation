using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906558_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_App_and_browser_Control
    {
        [Test]
        public async Task Test_8906558_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_App_and_browser_Control()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906558 completed");
        }
    }
}
