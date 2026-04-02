using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8898806_Device_Restrictions_Windows_Spotlight_Apps_suggestions_in_Ink_workspace
    {
        [Test]
        public async Task Test_8898806_Device_Restrictions_Windows_Spotlight_Apps_suggestions_in_Ink_workspace()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8898806 completed");
        }
    }
}
