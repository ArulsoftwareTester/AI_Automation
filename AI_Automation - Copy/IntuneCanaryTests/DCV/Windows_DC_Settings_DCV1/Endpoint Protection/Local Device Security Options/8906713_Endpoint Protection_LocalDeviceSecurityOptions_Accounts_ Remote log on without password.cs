using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906713_Endpoint_Protection_LocalDeviceSecurityOptions_Accounts_Remote_log_on_without_password : SecurityBaseline
    {
        [Test]
        public async Task Test_8906713_Endpoint_Protection_LocalDeviceSecurityOptions_Accounts_Remote_log_on_without_password()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8906713 completed");
        }
    }
}
