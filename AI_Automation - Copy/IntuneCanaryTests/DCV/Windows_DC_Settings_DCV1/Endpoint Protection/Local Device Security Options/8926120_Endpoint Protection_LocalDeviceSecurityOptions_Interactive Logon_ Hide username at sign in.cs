using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926120_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Hide_username_at_sign_in : SecurityBaseline
    {
        [Test]
        public async Task Test_8926120_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Hide_username_at_sign_in()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926120 completed");
        }
    }
}
