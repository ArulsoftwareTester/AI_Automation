using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898783_Device_Restrictions_Windows_Spotlight_Windows_Tips
    {
        [Test]
        public async Task Test_8898783_Device_Restrictions_Windows_Spotlight_Windows_Tips()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898783 completed");
        }
    }
}
