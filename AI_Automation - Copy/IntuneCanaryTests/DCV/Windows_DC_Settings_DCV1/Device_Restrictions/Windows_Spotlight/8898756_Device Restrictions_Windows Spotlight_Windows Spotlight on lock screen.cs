using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898756_Device_Restrictions_Windows_Spotlight_Windows_Spotlight_on_lock_screen
    {
        [Test]
        public async Task Test_8898756_Device_Restrictions_Windows_Spotlight_Windows_Spotlight_on_lock_screen()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898756 completed");
        }
    }
}
