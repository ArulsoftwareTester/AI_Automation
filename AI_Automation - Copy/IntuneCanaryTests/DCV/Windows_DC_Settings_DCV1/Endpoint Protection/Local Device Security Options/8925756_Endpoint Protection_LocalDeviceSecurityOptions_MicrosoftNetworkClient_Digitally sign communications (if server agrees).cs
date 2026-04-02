using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925756_Endpoint_Protection_LocalDeviceSecurityOptions_MicrosoftNetworkClient_Digitally_sign_communications_if_server_agrees : SecurityBaseline
    {
        [Test]
        public async Task Test_8925756_Endpoint_Protection_LocalDeviceSecurityOptions_MicrosoftNetworkClient_Digitally_sign_communications_if_server_agrees()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925756 completed");
        }
    }
}
