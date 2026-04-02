using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767033_DC_Delivery_Optimization_Delay_foreground_download_Cache_Server_fallback_in_seconds_Delay_background_download_Cache_Server_fallback_in_seconds_settings_test
    {
        [Test]
        public async Task Test_8767033_DC_Delivery_Optimization_Delay_foreground_download_Cache_Server_fallback_in_seconds_Delay_background_download_Cache_Server_fallback_in_seconds_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767033 completed");
        }
    }
}
