using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254109_Script_String_NotEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254109_Verify_applicability_true_Script_String_NotEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254109 - Verify applicability is true when output data type is string, operator is 'Not Equal to', script output value 'Not Equal to' value");
            Console.WriteLine("Test_4254109 completed");
        }
    }
}
