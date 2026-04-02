using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8926081_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Virtualize_file_and_registry_write_failures_to_peruser_locations : SecurityBaseline
    {
        [Test]
        public async Task Test_8926081_Endpoint_Protection_LocalDeviceSecurityOptions_UserAccountControl_Virtualize_file_and_registry_write_failures_to_peruser_locations()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8926081 completed");
        }
    }
}
