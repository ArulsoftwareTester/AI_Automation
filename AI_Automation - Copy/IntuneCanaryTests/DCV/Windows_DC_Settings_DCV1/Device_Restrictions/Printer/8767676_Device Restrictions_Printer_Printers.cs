using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767676_Device_Restrictions_Printer_Printers : SecurityBaseline
    {
        [Test]
        public async Task Test_8767676_Device_Restrictions_Printer_Printers()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8767676 completed");
        }
    }
}
