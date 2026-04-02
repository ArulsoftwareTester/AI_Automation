using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766922_DC_Delivery_Optimization_Maximum_cache_size_type_settings_test
    {
        [Test]
        public async Task Test_8766922_DC_Delivery_Optimization_Maximum_cache_size_type_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766922 completed");
        }
    }
}
