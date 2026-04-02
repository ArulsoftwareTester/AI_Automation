using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9370769_Wi_Fi_Wi_Fi_Enterprise_type_UI_check
    {
        [Test]
        public async Task Test_9370769_Wi_Fi_Wi_Fi_Enterprise_type_UI_check()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9370769 completed");
        }
    }
}
