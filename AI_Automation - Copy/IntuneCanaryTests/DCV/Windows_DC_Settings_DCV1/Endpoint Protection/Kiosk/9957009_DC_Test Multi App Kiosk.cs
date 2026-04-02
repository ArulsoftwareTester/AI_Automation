using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9957009_DC_Test_Multi_App_Kiosk
    {
        [Test]
        public async Task Test_9957009_DC_Test_Multi_App_Kiosk()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9957009 completed");
        }
    }
}
