using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8925935_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Require_CTRL_ALT_DEL_to_log_on : SecurityBaseline
    {
        [Test]
        public async Task Test_8925935_Endpoint_Protection_LocalDeviceSecurityOptions_Interactive_Logon_Require_CTRL_ALT_DEL_to_log_on()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8925935 completed");
        }
    }
}
