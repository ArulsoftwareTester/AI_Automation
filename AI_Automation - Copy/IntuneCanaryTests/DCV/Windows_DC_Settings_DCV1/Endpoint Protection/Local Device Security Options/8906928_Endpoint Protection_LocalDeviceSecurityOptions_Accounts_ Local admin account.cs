using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906928_Endpoint_Protection_LocalDeviceSecurityOptions_Accounts_Local_admin_account : SecurityBaseline
    {
        [Test]
        public async Task Test_8906928_Endpoint_Protection_LocalDeviceSecurityOptions_Accounts_Local_admin_account()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8906928 completed");
        }
    }
}
