using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8933779_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Minimum_Session_Security_For_NTLM_SSP_Based_Server : SecurityBaseline
    {
        [Test]
        public async Task Test_8933779_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Minimum_Session_Security_For_NTLM_SSP_Based_Server()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8933779 completed");
        }
    }
}
