using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767739_DC_Device_Restrictions_Start_Lock
    {
        [Test]
        public async Task Test_8767739_DC_Device_Restrictions_Start_Lock()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767739 completed");
        }
    }
}
