using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524908_DC_Exclude_via_Policy_Set1_Include_via_Policy_Set2
    {
        [Test]
        public async Task Test_5524908_DC_Exclude_via_Policy_Set1_Include_via_Policy_Set2()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524908 completed");
        }
    }
}
