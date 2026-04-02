using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297757_Wired_network_UX_validation_Maximum_authentication_failures : SecurityBaseline
    {
        [Test]
        public async Task Test_26297757_Wired_network_UX_validation_Maximum_authentication_failures()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297757 completed");
        }
    }
}
