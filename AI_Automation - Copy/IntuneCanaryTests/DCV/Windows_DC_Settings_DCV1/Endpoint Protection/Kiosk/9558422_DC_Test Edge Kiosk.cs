using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9558422_DC_Test_Edge_Kiosk
    {
        [Test]
        public async Task Test_9558422_DC_Test_Edge_Kiosk()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_9558422 completed");
        }
    }
}
