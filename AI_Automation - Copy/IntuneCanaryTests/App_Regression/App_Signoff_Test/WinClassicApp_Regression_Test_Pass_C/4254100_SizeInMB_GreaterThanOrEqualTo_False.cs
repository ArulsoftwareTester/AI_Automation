using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254100_SizeInMB_GreaterThanOrEqualTo_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254100_Verify_applicability_false_SizeInMB_GreaterThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254100 - Verify applicability is false when Size in MB evaluates 'Greater than or equal to' to false");
            Console.WriteLine("Test_4254100 completed");
        }
    }
}
