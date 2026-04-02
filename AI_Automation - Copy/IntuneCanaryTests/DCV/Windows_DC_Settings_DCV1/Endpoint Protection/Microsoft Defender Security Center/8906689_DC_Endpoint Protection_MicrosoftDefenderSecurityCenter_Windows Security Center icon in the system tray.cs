using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906689_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Windows_Security_Center_icon_in_the_system_tray
    {
        [Test]
        public async Task Test_8906689_DC_Endpoint_Protection_MicrosoftDefenderSecurityCenter_Windows_Security_Center_icon_in_the_system_tray()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8906689 completed");
        }
    }
}
