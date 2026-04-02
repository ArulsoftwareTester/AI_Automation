using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767682_Device_Restrictions_Printer_Default_printer : SecurityBaseline
    {
        [Test]
        public async Task Test_8767682_Device_Restrictions_Printer_Default_printer()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8767682 completed");
        }
    }
}
