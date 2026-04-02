using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524814_DC_Remove_assignment_from_Policy_Set
    {
        [Test]
        public async Task Test_5524814_DC_Remove_assignment_from_Policy_Set()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524814 completed");
        }
    }
}
