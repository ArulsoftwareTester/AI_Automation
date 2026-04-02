using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767621_DC_Device_Restrictions_Start_Fast_user_switching
    {
        [Test]
        public async Task Test_8767621_DC_Device_Restrictions_Start_Fast_user_switching()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767621 completed");
        }
    }
}
