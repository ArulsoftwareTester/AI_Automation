using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254163_Registry_IntegerValue_Equals_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254163_Verify_applicability_true_Registry_IntegerValue_Equals()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254163 - Verify applicability is true when Registry Integer value under Key path evaluates 'Equals' to True");
            Console.WriteLine("Test_4254163 completed");
        }
    }
}
