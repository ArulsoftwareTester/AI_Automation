using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8933850_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_LAN_Manager_Authentication_Level : SecurityBaseline
    {
        [Test]
        public async Task Test_8933850_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_LAN_Manager_Authentication_Level()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8933850 completed");
        }
    }
}
