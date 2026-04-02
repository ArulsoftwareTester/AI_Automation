using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254166_Registry_IntegerValue_NotEqualTo_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254166_Verify_applicability_false_Registry_IntegerValue_NotEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254166 - Verify applicability is false when Registry Integer value under Key path evaluates 'Not Equal to' to False");
            Console.WriteLine("Test_4254166 completed");
        }
    }
}
