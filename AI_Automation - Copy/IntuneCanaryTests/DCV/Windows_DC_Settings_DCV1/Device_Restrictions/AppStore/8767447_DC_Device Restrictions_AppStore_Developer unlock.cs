using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767447_DC_Device_Restrictions_AppStore_Developer_unlock
    {
        [Test]
        public async Task Test_8767447_DC_Device_Restrictions_AppStore_Developer_unlock()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767447 completed");
        }
    }
}
