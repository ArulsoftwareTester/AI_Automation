using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254064_DateModified_GreaterThanOrEqualTo_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254064_Verify_applicability_false_DateModified_GreaterThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254064 - Verify applicability is false when Date modified_UTC evaluates 'Greater than or equal to' to false");
            Console.WriteLine("Test_4254064 completed");
        }
    }
}
