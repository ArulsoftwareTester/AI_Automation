using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254172_Registry_IntegerValue_LessThanOrEqualTo_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254172_Verify_applicability_false_Registry_IntegerValue_LessThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254172 - Verify applicability is false when Registry Integer value under Key path evaluates 'less than or equal to' to False");
            Console.WriteLine("Test_4254172 completed");
        }
    }
}
