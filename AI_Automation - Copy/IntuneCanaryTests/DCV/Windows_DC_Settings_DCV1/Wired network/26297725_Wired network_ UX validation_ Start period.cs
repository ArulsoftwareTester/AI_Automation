using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297725_Wired_network_UX_validation_Start_period : SecurityBaseline
    {
        [Test]
        public async Task Test_26297725_Wired_network_UX_validation_Start_period()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297725 completed");
        }
    }
}
