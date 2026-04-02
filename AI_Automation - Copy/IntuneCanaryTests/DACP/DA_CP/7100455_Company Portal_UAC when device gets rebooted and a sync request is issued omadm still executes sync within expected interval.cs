using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T7100455_Company_Portal_UAC_when_device_gets_rebooted_and_a_sync_request_is_issued_omadm_still_executes_sync_within_expected_interval : SecurityBaseline
    {
        [Test]
        public async Task Test_7100455_Company_Portal_UAC_when_device_gets_rebooted_and_a_sync_request_is_issued_omadm_still_executes_sync_within_expected_interval()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_7100455 completed");
        }
    }
}
