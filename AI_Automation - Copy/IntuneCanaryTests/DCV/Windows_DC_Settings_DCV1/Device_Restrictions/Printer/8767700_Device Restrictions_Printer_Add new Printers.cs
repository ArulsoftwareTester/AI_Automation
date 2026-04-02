using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767700_Device_Restrictions_Printer_Add_new_Printers : SecurityBaseline
    {
        [Test]
        public async Task Test_8767700_Device_Restrictions_Printer_Add_new_Printers()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8767700 completed");
        }
    }
}
