using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767773_DC_Device_Restrictions_Start_Hibernate
    {
        [Test]
        public async Task Test_8767773_DC_Device_Restrictions_Start_Hibernate()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767773 completed");
        }
    }
}
