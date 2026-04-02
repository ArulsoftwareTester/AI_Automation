using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297647_Wired_network_UX_validation_Authentication_period : SecurityBaseline
    {
        [Test]
        public async Task Test_26297647_Wired_network_UX_validation_Authentication_period()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297647 completed");
        }
    }
}
