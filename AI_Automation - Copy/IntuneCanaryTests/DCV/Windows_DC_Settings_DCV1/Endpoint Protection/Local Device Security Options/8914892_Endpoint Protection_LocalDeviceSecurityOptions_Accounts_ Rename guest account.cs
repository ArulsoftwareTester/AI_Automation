using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8914892_Endpoint_Protection_LocalDeviceSecurityOptions_Accounts_Rename_guest_account : SecurityBaseline
    {
        [Test]
        public async Task Test_8914892_Endpoint_Protection_LocalDeviceSecurityOptions_Accounts_Rename_guest_account()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8914892 completed");
        }
    }
}
