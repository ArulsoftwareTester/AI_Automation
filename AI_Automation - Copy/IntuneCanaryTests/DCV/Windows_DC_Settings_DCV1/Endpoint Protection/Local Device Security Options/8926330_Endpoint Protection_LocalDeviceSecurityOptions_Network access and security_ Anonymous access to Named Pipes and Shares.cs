using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926330_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Anonymous_access_to_Named_Pipes_and_Shares : SecurityBaseline
    {
        [Test]
        public async Task Test_8926330_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Anonymous_access_to_Named_Pipes_and_Shares()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926330 completed");
        }
    }
}
