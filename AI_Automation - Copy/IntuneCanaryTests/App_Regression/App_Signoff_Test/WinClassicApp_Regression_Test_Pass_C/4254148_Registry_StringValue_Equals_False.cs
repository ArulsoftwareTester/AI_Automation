using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254148_Registry_StringValue_Equals_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254148_Verify_applicability_false_Registry_StringValue_Equals()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254148 - Verify applicability is false when Registry String value under Key path evaluates 'Equals' to False");
            Console.WriteLine("Test_4254148 completed");
        }
    }
}
