using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926321_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Login_message_text : SecurityBaseline
    {
        [Test]
        public async Task Test_8926321_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Login_message_text()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926321 completed");
        }
    }
}
