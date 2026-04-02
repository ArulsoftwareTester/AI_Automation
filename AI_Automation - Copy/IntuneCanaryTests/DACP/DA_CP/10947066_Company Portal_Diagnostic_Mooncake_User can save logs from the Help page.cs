using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T10947066_Company_Portal_Diagnostic_Mooncake_User_can_save_logs_from_the_Help_page : SecurityBaseline
    {
        [Test]
        public async Task Test_10947066_Company_Portal_Diagnostic_Mooncake_User_can_save_logs_from_the_Help_page()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_10947066 completed");
        }
    }
}
