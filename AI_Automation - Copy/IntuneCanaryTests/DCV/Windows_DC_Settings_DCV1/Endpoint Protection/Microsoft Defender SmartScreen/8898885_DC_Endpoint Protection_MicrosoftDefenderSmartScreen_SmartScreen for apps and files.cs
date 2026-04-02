using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898885_DC_Endpoint_Protection_MicrosoftDefenderSmartScreen_SmartScreen_for_apps_and_files
    {
        [Test]
        public async Task Test_8898885_DC_Endpoint_Protection_MicrosoftDefenderSmartScreen_SmartScreen_for_apps_and_files()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898885 completed");
        }
    }
}
