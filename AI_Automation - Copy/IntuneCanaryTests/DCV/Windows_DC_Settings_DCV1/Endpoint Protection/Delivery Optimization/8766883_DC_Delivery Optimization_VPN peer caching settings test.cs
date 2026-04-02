using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8766883_DC_Delivery_Optimization_VPN_peer_caching_settings_test
    {
        [Test]
        public async Task Test_8766883_DC_Delivery_Optimization_VPN_peer_caching_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8766883 completed");
        }
    }
}
