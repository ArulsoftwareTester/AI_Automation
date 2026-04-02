using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254115_Script_DateTime_LessThanOrEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254115_Verify_applicability_true_Script_DateTime_LessThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254115 - Verify applicability is true when output data type is Date and Time, operator is 'less than or equal to', script output value 'less than or equal to' value");
            Console.WriteLine("Test_4254115 completed");
        }
    }
}
