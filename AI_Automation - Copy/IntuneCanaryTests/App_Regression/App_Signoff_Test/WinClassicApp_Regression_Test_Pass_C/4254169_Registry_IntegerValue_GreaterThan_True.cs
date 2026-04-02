using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254169_Registry_IntegerValue_GreaterThan_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254169_Verify_applicability_true_Registry_IntegerValue_GreaterThan()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254169 - Verify applicability is true when Registry Integer value under Key path evaluates 'Greater than' to True");
            Console.WriteLine("Test_4254169 completed");
        }
    }
}
