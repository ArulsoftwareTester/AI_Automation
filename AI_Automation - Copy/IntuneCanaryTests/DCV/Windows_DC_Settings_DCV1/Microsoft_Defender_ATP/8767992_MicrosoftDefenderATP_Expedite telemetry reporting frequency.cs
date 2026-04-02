using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767992_MicrosoftDefenderATP_Expedite_telemetry_reporting_frequency
    {
        [Test]
        public async Task Test_8767992_MicrosoftDefenderATP_Expedite_telemetry_reporting_frequency()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767992 completed");
        }
    }
}
