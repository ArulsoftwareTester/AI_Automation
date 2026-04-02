using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766846_DC_Delivery_Optimization_Caching_settings_test
    {
        [Test]
        public async Task Test_8766846_DC_Delivery_Optimization_Caching_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766846 completed");
        }
    }
}
