using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297791_Wired_network_UX_validation_Block_period_minutes : SecurityBaseline
    {
        [Test]
        public async Task Test_26297791_Wired_network_UX_validation_Block_period_minutes()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297791 completed");
        }
    }
}
