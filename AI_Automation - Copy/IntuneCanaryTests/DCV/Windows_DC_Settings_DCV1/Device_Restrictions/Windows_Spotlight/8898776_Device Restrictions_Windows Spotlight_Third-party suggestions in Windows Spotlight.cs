using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898776_Device_Restrictions_Windows_Spotlight_Third_party_suggestions_in_Windows_Spotlight
    {
        [Test]
        public async Task Test_8898776_Device_Restrictions_Windows_Spotlight_Third_party_suggestions_in_Windows_Spotlight()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898776 completed");
        }
    }
}
