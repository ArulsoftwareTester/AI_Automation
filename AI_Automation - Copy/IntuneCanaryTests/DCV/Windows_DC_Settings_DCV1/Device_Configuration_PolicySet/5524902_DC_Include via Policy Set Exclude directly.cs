using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524902_DC_Include_via_Policy_Set_Exclude_directly
    {
        [Test]
        public async Task Test_5524902_DC_Include_via_Policy_Set_Exclude_directly()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524902 completed");
        }
    }
}
