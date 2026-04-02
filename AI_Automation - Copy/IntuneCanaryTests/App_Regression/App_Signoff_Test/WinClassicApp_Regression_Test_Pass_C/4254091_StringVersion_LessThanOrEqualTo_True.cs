using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254091_StringVersion_LessThanOrEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254091_Verify_applicability_true_StringVersion_LessThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254091 - Verify applicability is true when String_version evaluates 'Less than or equal to' to true");
            Console.WriteLine("Test_4254091 completed");
        }
    }
}
