using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926365_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Anonymous_enumeration_of_SAM_accounts : SecurityBaseline
    {
        [Test]
        public async Task Test_8926365_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Anonymous_enumeration_of_SAM_accounts()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926365 completed");
        }
    }
}
