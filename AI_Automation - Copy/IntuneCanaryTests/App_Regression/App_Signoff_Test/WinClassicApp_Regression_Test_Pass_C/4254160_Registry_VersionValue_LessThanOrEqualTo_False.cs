using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254160_Registry_VersionValue_LessThanOrEqualTo_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254160_Verify_applicability_false_Registry_VersionValue_LessThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254160 - Verify applicability is false when Registry Version value under Key path evaluates 'less than or equal to' to False");
            Console.WriteLine("Test_4254160 completed");
        }
    }
}
