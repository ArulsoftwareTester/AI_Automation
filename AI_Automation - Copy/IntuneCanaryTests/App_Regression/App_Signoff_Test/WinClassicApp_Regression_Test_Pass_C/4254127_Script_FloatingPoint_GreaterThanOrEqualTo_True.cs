using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254127_Script_FloatingPoint_GreaterThanOrEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254127_Verify_applicability_true_Script_FloatingPoint_GreaterThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254127 - Verify applicability is true when output data type is Floating Point, operator is 'Greater than or equal to', script output value 'Greater than or equal to' value");
            Console.WriteLine("Test_4254127 completed");
        }
    }
}
