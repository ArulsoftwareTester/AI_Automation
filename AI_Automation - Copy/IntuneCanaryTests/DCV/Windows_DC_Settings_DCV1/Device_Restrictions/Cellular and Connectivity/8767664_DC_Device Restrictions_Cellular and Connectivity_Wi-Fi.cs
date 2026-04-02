using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767664_DC_Device_Restrictions_Cellular_and_Connectivity_Wi_Fi
    {
        [Test]
        public async Task Test_8767664_DC_Device_Restrictions_Cellular_and_Connectivity_Wi_Fi()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767664 completed");
        }
    }
}
