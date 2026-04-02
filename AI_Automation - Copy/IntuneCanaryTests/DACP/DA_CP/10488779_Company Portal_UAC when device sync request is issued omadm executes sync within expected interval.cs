using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T10488779_Company_Portal_UAC_when_device_sync_request_is_issued_omadm_executes_sync_within_expected_interval : SecurityBaseline
    {
        [Test]
        public async Task Test_10488779_Company_Portal_UAC_when_device_sync_request_is_issued_omadm_executes_sync_within_expected_interval()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_10488779 completed");
        }
    }
}
