using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524208_DC_Prepare_step_for_PolicySet_Test_cases
    {
        [Test]
        public async Task Test_5524208_DC_Prepare_step_for_PolicySet_Test_cases()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524208 completed");
        }
    }
}
