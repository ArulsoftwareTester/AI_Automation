using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767005_DC_Delivery_Optimization_Cache_server_fully_qualified_domain_names_FQDN_or_IP_addresses_settings_test
    {
        [Test]
        public async Task Test_8767005_DC_Delivery_Optimization_Cache_server_fully_qualified_domain_names_FQDN_or_IP_addresses_settings_test()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767005 completed");
        }
    }
}
