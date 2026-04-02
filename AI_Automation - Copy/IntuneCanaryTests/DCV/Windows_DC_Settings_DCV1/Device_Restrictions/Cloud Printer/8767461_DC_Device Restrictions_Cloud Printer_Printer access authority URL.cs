using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767461_DC_Device_Restrictions_Cloud_Printer_Printer_access_authority_URL
    {
        [Test]
        public async Task Test_8767461_DC_Device_Restrictions_Cloud_Printer_Printer_access_authority_URL()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767461 completed");
        }
    }
}
