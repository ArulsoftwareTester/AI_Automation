using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778644_Device_Restrictions_Reporting_and_Telemetry_Telemetry_proxy_server : SecurityBaseline
    {
        [Test]
        public async Task Test_8778644_Device_Restrictions_Reporting_and_Telemetry_Telemetry_proxy_server()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8778644 completed");
        }
    }
}
