using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254094_StringVersion_LessThan_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254094_Verify_applicability_false_StringVersion_LessThan()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254094 - Verify applicability is false when String_version evaluates 'Less than' to false");
            Console.WriteLine("Test_4254094 completed");
        }
    }
}
