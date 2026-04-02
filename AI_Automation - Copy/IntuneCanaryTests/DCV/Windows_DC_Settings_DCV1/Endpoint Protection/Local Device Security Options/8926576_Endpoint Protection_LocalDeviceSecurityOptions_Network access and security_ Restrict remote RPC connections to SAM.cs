using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926576_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Restrict_remote_RPC_connections_to_SAM : SecurityBaseline
    {
        [Test]
        public async Task Test_8926576_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Restrict_remote_RPC_connections_to_SAM()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926576 completed");
        }
    }
}
