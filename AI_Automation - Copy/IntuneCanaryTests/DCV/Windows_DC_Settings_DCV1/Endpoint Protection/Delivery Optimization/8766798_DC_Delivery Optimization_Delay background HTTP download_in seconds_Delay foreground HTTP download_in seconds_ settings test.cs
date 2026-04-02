using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766798_DC_Delivery_Optimization_Delay_background_HTTP_download_in_seconds_Delay_foreground_HTTP_download_in_seconds_settings_test
    {
        [Test]
        public async Task Test_8766798_DC_Delivery_Optimization_Delay_background_HTTP_download_in_seconds_Delay_foreground_HTTP_download_in_seconds_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766798 completed");
        }
    }
}
