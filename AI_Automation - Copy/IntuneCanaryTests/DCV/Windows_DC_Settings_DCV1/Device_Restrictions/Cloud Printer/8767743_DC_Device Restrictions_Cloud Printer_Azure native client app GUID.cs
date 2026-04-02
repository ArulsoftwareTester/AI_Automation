using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767743_DC_Device_Restrictions_Cloud_Printer_Azure_native_client_app_GUID
    {
        [Test]
        public async Task Test_8767743_DC_Device_Restrictions_Cloud_Printer_Azure_native_client_app_GUID()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767743 completed");
        }
    }
}
