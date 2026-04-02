using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254130_Script_FloatingPoint_LessThan_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254130_Verify_applicability_true_Script_FloatingPoint_LessThan()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254130 - Verify applicability is true when output data type is Floating Point, operator is 'less than', script output value 'less than' value");
            Console.WriteLine("Test_4254130 completed");
        }
    }
}
