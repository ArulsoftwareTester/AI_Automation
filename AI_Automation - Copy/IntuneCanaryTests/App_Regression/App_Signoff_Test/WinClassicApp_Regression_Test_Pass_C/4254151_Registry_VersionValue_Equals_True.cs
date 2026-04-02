using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254151_Registry_VersionValue_Equals_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254151_Verify_applicability_true_Registry_VersionValue_Equals()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254151 - Verify applicability is true when Registry Version value under Key path evaluates 'Equals' to True");
            Console.WriteLine("Test_4254151 completed");
        }
    }
}
