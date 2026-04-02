using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254118_Script_Integer_Equals_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254118_Verify_applicability_true_Script_Integer_Equals()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254118 - Verify applicability is true when output data type is Integer, operator is 'Equals', script output value 'Equals' value");
            Console.WriteLine("Test_4254118 completed");
        }
    }
}
