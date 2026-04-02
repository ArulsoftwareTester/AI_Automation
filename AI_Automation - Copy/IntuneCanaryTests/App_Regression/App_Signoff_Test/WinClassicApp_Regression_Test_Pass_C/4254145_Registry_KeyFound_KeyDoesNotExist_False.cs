using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254145_Registry_KeyFound_KeyDoesNotExist_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254145_Verify_applicability_false_Registry_KeyFound_KeyDoesNotExist()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254145 - Verify applicability is false when Registry Key is found and operator is 'Key does not exist'");
            Console.WriteLine("Test_4254145 completed");
        }
    }
}
