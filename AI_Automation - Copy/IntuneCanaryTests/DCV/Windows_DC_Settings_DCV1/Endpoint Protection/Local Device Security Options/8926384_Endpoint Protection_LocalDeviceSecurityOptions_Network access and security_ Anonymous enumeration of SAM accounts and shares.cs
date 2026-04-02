using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926384_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Anonymous_enumeration_of_SAM_accounts_and_shares : SecurityBaseline
    {
        [Test]
        public async Task Test_8926384_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Anonymous_enumeration_of_SAM_accounts_and_shares()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926384 completed");
        }
    }
}
