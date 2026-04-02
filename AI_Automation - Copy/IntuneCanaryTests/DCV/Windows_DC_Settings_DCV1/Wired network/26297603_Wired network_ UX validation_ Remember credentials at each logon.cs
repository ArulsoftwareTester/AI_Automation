using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26297603_Wired_network_UX_validation_Remember_credentials_at_each_logon : SecurityBaseline
    {
        [Test]
        public async Task Test_26297603_Wired_network_UX_validation_Remember_credentials_at_each_logon()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26297603 completed");
        }
    }
}
