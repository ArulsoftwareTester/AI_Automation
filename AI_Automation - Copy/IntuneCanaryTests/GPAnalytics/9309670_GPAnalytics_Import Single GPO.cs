using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9309670_GPAnalytics_Import_Single_GPO : PageTest
    {
        [Test]
        public async Task Test_9309670_GPAnalytics_Import_Single_GPO()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_9309670 completed");
        }
    }
}
