using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926532_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_LAN_Manager_hash_value_stored_on_password_change : SecurityBaseline
    {
        [Test]
        public async Task Test_8926532_Endpoint_Protection_LocalDeviceSecurityOptions_Network_access_and_security_LAN_Manager_hash_value_stored_on_password_change()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926532 completed");
        }
    }
}
