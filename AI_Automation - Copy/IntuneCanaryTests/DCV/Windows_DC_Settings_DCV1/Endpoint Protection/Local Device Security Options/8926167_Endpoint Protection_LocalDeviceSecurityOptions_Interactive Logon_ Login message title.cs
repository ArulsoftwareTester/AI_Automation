using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926167_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Login_message_title : SecurityBaseline
    {
        [Test]
        public async Task Test_8926167_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Login_message_title()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926167 completed");
        }
    }
}
