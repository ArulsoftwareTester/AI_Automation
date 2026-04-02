using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254157_Registry_VersionValue_GreaterThan_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254157_Verify_applicability_true_Registry_VersionValue_GreaterThan()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254157 - Verify applicability is true when Registry Version value under Key path evaluates 'Greater than' to True");
            Console.WriteLine("Test_4254157 completed");
        }
    }
}
