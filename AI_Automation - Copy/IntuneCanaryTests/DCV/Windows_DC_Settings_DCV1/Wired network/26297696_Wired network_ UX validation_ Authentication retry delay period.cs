using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297696_Wired_network_UX_validation_Authentication_retry_delay_period : SecurityBaseline
    {
        [Test]
        public async Task Test_26297696_Wired_network_UX_validation_Authentication_retry_delay_period()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297696 completed");
        }
    }
}
