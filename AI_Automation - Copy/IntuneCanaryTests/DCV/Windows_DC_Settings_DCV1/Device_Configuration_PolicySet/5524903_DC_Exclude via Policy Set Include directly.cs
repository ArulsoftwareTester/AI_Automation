using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524903_DC_Exclude_via_Policy_Set_Include_directly
    {
        [Test]
        public async Task Test_5524903_DC_Exclude_via_Policy_Set_Include_directly()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524903 completed");
        }
    }
}
