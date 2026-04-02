using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926098_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Hide_last_signedin_user : SecurityBaseline
    {
        [Test]
        public async Task Test_8926098_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Hide_last_signedin_user()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926098 completed");
        }
    }
}
