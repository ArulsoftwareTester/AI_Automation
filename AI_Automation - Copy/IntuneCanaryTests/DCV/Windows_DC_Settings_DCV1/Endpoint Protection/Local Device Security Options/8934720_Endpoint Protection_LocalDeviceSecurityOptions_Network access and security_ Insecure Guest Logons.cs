using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8934720_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Insecure_Guest_Logons : SecurityBaseline
    {
        [Test]
        public async Task Test_8934720_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_Insecure_Guest_Logons()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8934720 completed");
        }
    }
}
