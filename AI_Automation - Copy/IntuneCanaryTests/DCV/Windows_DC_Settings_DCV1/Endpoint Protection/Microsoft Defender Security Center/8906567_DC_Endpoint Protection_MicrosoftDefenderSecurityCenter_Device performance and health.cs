using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906567_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Device_performance_and_health
    {
        [Test]
        public async Task Test_8906567_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Device_performance_and_health()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906567 completed");
        }
    }
}
