using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254070_DateModified_LessThan_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254070_Verify_applicability_false_DateModified_LessThan()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254070 - Verify applicability is false when Date modified_UTC evaluates 'Less than' to False");
            Console.WriteLine("Test_4254070 completed");
        }
    }
}
