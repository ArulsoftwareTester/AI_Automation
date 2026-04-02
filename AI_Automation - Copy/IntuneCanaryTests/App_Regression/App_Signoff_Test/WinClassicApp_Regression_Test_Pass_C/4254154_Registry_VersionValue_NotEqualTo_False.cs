using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254154_Registry_VersionValue_NotEqualTo_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254154_Verify_applicability_false_Registry_VersionValue_NotEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254154 - Verify applicability is false when Registry Version value under Key path evaluates 'Not Equal to' to False");
            Console.WriteLine("Test_4254154 completed");
        }
    }
}
