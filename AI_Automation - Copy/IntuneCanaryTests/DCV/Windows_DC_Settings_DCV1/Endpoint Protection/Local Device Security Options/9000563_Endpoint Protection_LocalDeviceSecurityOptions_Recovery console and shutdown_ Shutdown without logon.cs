using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9000563_Endpoint_Protection_LocalDeviceSecurityOptions_Recovery_console_and_shutdown_Shutdown_without_logon : SecurityBaseline
    {
        [Test]
        public async Task Test_9000563_Endpoint_Protection_LocalDeviceSecurityOptions_Recovery_console_and_shutdown_Shutdown_without_logon()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_9000563 completed");
        }
    }
}
