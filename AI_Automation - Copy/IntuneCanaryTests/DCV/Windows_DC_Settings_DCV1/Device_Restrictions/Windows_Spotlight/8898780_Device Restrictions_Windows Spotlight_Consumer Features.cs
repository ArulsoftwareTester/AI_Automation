using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898780_Device_Restrictions_Windows_Spotlight_Consumer_Features
    {
        [Test]
        public async Task Test_8898780_Device_Restrictions_Windows_Spotlight_Consumer_Features()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898780 completed");
        }
    }
}
