using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926040_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_User_information_on_lock_screen : SecurityBaseline
    {
        [Test]
        public async Task Test_8926040_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_User_information_on_lock_screen()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926040 completed");
        }
    }
}
