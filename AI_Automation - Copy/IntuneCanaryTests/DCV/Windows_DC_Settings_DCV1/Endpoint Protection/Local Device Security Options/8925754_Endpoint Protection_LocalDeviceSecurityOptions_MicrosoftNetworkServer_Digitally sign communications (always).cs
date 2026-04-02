using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925754_Endpoint_Protection_LocalDeviceSecurityOptions_MicrosoftNetworkServer_Digitally_sign_communications_always : SecurityBaseline
    {
        [Test]
        public async Task Test_8925754_Endpoint_Protection_LocalDeviceSecurityOptions_MicrosoftNetworkServer_Digitally_sign_communications_always()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925754 completed");
        }
    }
}
