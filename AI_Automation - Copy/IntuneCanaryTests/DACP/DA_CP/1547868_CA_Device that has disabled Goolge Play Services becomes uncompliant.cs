using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1547868_CA_Device_that_has_disabled_Goolge_Play_Services_becomes_uncompliant : SecurityBaseline
    {
        [Test]
        public async Task Test_1547868_CA_Device_that_has_disabled_Goolge_Play_Services_becomes_uncompliant()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1547868 completed");
        }
    }
}
