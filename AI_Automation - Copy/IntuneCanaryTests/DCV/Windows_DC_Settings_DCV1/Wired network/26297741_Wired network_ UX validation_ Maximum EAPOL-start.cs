using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297741_Wired_network_UX_validation_Maximum_EAPOLstart : SecurityBaseline
    {
        [Test]
        public async Task Test_26297741_Wired_network_UX_validation_Maximum_EAPOLstart()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297741 completed");
        }
    }
}
