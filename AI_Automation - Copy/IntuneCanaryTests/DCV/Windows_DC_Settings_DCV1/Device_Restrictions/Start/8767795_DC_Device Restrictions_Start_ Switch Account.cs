using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767795_DC_Device_Restrictions_Start_Switch_Account
    {
        [Test]
        public async Task Test_8767795_DC_Device_Restrictions_Start_Switch_Account()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767795 completed");
        }
    }
}
