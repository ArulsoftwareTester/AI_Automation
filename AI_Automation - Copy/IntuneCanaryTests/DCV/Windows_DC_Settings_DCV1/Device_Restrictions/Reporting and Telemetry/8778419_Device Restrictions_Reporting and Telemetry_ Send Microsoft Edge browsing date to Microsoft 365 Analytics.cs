using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8778419_Device_Restrictions_Reporting_and_Telemetry_Send_Microsoft_Edge_browsing_date_to_Microsoft_365_Analytics : SecurityBaseline
    {
        [Test]
        public async Task Test_8778419_Device_Restrictions_Reporting_and_Telemetry_Send_Microsoft_Edge_browsing_date_to_Microsoft_365_Analytics()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8778419 completed");
        }
    }
}
