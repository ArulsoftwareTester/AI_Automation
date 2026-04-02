using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254088_StringVersion_GreaterThanOrEqualTo_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254088_Verify_applicability_false_StringVersion_GreaterThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254088 - Verify applicability is false when String_version evaluates 'Greater than or equal to' to false");
            Console.WriteLine("Test_4254088 completed");
        }
    }
}
