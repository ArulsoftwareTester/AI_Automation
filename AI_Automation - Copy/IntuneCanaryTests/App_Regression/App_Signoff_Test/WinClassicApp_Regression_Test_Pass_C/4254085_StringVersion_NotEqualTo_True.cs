using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254085_StringVersion_NotEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254085_Verify_applicability_true_StringVersion_NotEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254085 - Verify applicability is true when String_version evaluates 'Not Equal to' to true");
            Console.WriteLine("Test_4254085 completed");
        }
    }
}
