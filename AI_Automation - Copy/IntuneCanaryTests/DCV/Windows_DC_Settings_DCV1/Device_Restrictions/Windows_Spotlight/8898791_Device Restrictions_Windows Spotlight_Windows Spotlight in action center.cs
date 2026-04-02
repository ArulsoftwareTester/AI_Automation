using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898791_Device_Restrictions_Windows_Spotlight_Windows_Spotlight_in_action_center
    {
        [Test]
        public async Task Test_8898791_Device_Restrictions_Windows_Spotlight_Windows_Spotlight_in_action_center()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898791 completed");
        }
    }
}
