using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767754_DC_Device_Restrictions_Start_Shut_Down
    {
        [Test]
        public async Task Test_8767754_DC_Device_Restrictions_Start_Shut_Down()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767754 completed");
        }
    }
}
