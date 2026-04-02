using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925758_Endpoint_Protection_LocalDeviceSecurityOptions_MicrosoftNetworkClient_Send_unencrypted_password_to_thirdparty_SMB_servers : SecurityBaseline
    {
        [Test]
        public async Task Test_8925758_Endpoint_Protection_LocalDeviceSecurityOptions_MicrosoftNetworkClient_Send_unencrypted_password_to_thirdparty_SMB_servers()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925758 completed");
        }
    }
}
