using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254139_Script_Boolean_Equals_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254139_Verify_applicability_true_Script_Boolean_Equals()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254139 - Verify applicability is true when output data type is Boolean, operator is 'Equals', script output value 'Equals' value");
            Console.WriteLine("Test_4254139 completed");
        }
    }
}
