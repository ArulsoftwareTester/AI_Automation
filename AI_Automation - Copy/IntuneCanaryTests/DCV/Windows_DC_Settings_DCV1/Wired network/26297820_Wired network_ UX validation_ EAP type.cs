using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297820_Wired_network_UX_validation_EAP_type : SecurityBaseline
    {
        [Test]
        public async Task Test_26297820_Wired_network_UX_validation_EAP_type()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297820 completed");
        }
    }
}
