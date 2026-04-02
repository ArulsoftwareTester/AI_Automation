using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297772_Wired_network_UX_validation_8021x : SecurityBaseline
    {
        [Test]
        public async Task Test_26297772_Wired_network_UX_validation_8021x()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297772 completed");
        }
    }
}
