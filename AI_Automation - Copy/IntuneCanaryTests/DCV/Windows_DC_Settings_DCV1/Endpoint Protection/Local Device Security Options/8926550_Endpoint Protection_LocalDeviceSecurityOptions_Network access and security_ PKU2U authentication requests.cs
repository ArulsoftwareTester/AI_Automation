using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926550_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_PKU2U_authentication_requests : SecurityBaseline
    {
        [Test]
        public async Task Test_8926550_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_PKU2U_authentication_requests()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926550 completed");
        }
    }
}
