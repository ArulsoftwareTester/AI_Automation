using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906761_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Notifications_from_the_displayed_areas_of_app
    {
        [Test]
        public async Task Test_8906761_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Notifications_from_the_displayed_areas_of_app()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906761 completed");
        }
    }
}
