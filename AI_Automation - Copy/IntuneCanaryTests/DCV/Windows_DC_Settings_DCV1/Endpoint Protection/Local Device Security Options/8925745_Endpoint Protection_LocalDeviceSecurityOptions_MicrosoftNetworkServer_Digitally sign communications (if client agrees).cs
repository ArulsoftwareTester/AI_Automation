using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925745_Endpoint_Protection_LocalDeviceSecurityOptions_MicrosoftNetworkServer_Digitally_sign_communications_if_client_agrees : SecurityBaseline
    {
        [Test]
        public async Task Test_8925745_Endpoint_Protection_LocalDeviceSecurityOptions_MicrosoftNetworkServer_Digitally_sign_communications_if_client_agrees()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925745 completed");
        }
    }
}
