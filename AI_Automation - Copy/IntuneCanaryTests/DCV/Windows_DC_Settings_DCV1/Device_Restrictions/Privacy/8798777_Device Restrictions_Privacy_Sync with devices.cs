using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8798777_Device_Restrictions_Privacy_Sync_with_devices : SecurityBaseline
    {
        [Test]
        public async Task Test_8798777()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8798777 completed");
        }
    }
}
