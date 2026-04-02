using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2306333_GPS_Win32_Settings_List_Box : PageTest
    {
        [Test]
        public async Task Test_2306333_GPS_Win32_Settings_List_Box()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(Page);
            Console.WriteLine("Test_2306333 completed");
        }
    }
}
