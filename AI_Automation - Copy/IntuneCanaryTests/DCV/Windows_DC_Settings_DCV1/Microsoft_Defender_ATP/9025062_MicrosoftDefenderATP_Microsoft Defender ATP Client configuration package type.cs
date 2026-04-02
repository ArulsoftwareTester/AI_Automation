using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9025062_MicrosoftDefenderATP_Microsoft_Defender_ATP_Client_configuration_package_type
    {
        [Test]
        public async Task Test_9025062_MicrosoftDefenderATP_Microsoft_Defender_ATP_Client_configuration_package_type()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9025062 completed");
        }
    }
}
