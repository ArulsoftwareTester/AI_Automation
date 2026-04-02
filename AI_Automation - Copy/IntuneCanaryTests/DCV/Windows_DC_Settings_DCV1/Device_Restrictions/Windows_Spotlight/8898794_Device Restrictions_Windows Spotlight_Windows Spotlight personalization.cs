using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898794_Device_Restrictions_Windows_Spotlight_Windows_Spotlight_personalization
    {
        [Test]
        public async Task Test_8898794_Device_Restrictions_Windows_Spotlight_Windows_Spotlight_personalization()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898794 completed");
        }
    }
}
