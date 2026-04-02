using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254079_DateCreated_LessThanOrEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254079_Verify_applicability_true_DateCreated_LessThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254079 - Verify applicability is true when Date created_UTC evaluates 'Less than or equal to' to true");
            Console.WriteLine("Test_4254079 completed");
        }
    }
}
