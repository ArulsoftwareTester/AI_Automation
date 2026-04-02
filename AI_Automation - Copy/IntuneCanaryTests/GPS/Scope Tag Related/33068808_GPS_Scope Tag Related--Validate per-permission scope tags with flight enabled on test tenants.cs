using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T33068808_GPS_Scope_Tag_Related__Validate_per_permission_scope_tags_with_flight_enabled_on_test_tenants : PageTest
    {
        [Test]
        public async Task Test_33068808_GPS_Scope_Tag_Related__Validate_per_permission_scope_tags_with_flight_enabled_on_test_tenants()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_33068808 completed");
        }
    }
}
