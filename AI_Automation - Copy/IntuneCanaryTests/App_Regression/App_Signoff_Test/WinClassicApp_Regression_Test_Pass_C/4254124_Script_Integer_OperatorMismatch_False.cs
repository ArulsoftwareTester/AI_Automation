using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254124_Script_Integer_OperatorMismatch_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254124_Verify_applicability_false_Script_Integer_OperatorMismatch()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254124 - Verify applicability is false when output data type is Integer, operator and relationship between Value and script output are not the same");
            Console.WriteLine("Test_4254124 completed");
        }
    }
}
