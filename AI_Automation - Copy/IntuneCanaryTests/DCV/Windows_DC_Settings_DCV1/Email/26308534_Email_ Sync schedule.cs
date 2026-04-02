using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26308534_Email_Sync_schedule : SecurityBaseline
    {
        [Test]
        public async Task Test_26308534_Email_Sync_schedule()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_26308534 completed");
        }
    }
}
