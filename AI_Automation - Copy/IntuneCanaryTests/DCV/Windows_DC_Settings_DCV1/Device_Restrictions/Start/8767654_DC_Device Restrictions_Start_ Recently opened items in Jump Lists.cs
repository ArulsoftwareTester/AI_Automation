using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767654_DC_Device_Restrictions_Start_Recently_opened_items_in_Jump_Lists
    {
        [Test]
        public async Task Test_8767654_DC_Device_Restrictions_Start_Recently_opened_items_in_Jump_Lists()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767654 completed");
        }
    }
}
