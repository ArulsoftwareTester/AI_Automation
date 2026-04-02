using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926615_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Minimum_Session_Security_For_NTLM_SSP_Based_Clients : SecurityBaseline
    {
        [Test]
        public async Task Test_8926615_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Minimum_Session_Security_For_NTLM_SSP_Based_Clients()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926615 completed");
        }
    }
}
