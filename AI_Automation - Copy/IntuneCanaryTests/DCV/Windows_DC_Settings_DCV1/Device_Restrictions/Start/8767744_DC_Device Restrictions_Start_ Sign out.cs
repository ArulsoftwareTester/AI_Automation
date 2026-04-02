using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767744_DC_Device_Restrictions_Start_Sign_out
    {
        [Test]
        public async Task Test_8767744_DC_Device_Restrictions_Start_Sign_out()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767744 completed");
        }
    }
}
