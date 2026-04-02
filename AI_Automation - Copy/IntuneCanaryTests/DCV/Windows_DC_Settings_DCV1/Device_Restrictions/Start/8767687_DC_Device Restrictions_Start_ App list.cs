using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767687_DC_Device_Restrictions_Start_App_list
    {
        [Test]
        public async Task Test_8767687_DC_Device_Restrictions_Start_App_list()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767687 completed");
        }
    }
}
