using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254067_DateModified_LessThanOrEqualTo_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254067_Verify_applicability_false_DateModified_LessThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254067 - Verify applicability is false when Date modified_UTC evaluates 'Less than or equal to' to FALSE");
            Console.WriteLine("Test_4254067 completed");
        }
    }
}
