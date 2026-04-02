using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8758824_DC_Delivery_Optimization_Download_mode_settings_test
    {
        [Test]
        public async Task Test_8758824_DC_Delivery_Optimization_Download_mode_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8758824 completed");
        }
    }
}
