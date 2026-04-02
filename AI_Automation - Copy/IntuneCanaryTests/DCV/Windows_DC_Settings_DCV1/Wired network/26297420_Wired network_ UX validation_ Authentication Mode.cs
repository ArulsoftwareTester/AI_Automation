using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297420_Wired_network_UX_validation_Authentication_Mode : SecurityBaseline
    {
        [Test]
        public async Task Test_26297420_Wired_network_UX_validation_Authentication_Mode()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297420 completed");
        }
    }
}
