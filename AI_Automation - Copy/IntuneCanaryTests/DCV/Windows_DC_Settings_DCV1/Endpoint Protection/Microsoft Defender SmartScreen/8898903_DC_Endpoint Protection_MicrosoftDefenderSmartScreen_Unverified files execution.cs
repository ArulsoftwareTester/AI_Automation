using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898903_DC_Endpoint_Protection_MicrosoftDefenderSmartScreen_Unverified_files_execution
    {
        [Test]
        public async Task Test_8898903_DC_Endpoint_Protection_MicrosoftDefenderSmartScreen_Unverified_files_execution()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898903 completed");
        }
    }
}
