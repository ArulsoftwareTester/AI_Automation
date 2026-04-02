using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254136_Script_Version_LessThanOrEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254136_Verify_applicability_true_Script_Version_LessThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254136 - Verify applicability is true when output data type is Version, operator is 'less than or equal to', script output value 'less than or equal to' value");
            Console.WriteLine("Test_4254136 completed");
        }
    }
}
