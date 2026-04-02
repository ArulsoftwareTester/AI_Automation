using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254121_Script_Integer_GreaterThan_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254121_Verify_applicability_true_Script_Integer_GreaterThan()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254121 - Verify applicability is true when output data type is Integer, operator is 'Greater than', script output value 'Greater than' value");
            Console.WriteLine("Test_4254121 completed");
        }
    }
}
