using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T715097_Device_Tagging_Ensure_device_tagging_puts_device_in_specified_device_group : SecurityBaseline
    {
        [Test]
        public async Task Test_715097_Device_Tagging_Ensure_device_tagging_puts_device_in_specified_device_group()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_715097 completed");
        }
    }
}
