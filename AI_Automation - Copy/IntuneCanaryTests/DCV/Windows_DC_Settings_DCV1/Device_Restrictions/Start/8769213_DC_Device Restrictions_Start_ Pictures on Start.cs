using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8769213_DC_Device_Restrictions_Start_Pictures_on_Start
    {
        [Test]
        public async Task Test_8769213_DC_Device_Restrictions_Start_Pictures_on_Start()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8769213 completed");
        }
    }
}
