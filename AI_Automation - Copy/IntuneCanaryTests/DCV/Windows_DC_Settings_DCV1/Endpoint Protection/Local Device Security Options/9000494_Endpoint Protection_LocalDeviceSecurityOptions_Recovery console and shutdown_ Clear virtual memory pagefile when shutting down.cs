using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9000494_Endpoint_Protection_LocalDeviceSecurityOptions_Recovery_console_and_shutdown_Clear_virtual_memory_pagefile_when_shutting_down : SecurityBaseline
    {
        [Test]
        public async Task Test_9000494_Endpoint_Protection_LocalDeviceSecurityOptions_Recovery_console_and_shutdown_Clear_virtual_memory_pagefile_when_shutting_down()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_9000494 completed");
        }
    }
}
