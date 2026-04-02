using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8822245_Device_Restrictions_Windows_Spotlight_Windows_Spotlight
    {
        [Test]
        public async Task Test_8822245_Device_Restrictions_Windows_Spotlight_Windows_Spotlight()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8822245 completed");
        }
    }
}
