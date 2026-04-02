using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524132_DC_Policy_Set_new_assignment
    {
        [Test]
        public async Task Test_5524132_DC_Policy_Set_new_assignment()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524132 completed");
        }
    }
}
