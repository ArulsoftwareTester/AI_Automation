using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8907034_Endpoint_Protection_LocalDeviceSecurityOptions_Accounts_Guest_account : SecurityBaseline
    {
        [Test]
        public async Task Test_8907034_Endpoint_Protection_LocalDeviceSecurityOptions_Accounts_Guest_account()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8907034 completed");
        }
    }
}
