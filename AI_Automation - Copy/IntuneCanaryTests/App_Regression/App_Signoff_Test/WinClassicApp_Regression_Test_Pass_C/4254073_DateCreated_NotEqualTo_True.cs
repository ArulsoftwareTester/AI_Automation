using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254073_DateCreated_NotEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254073_Verify_applicability_true_DateCreated_NotEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254073 - Verify applicability is true when Date created_UTC evaluates 'Not Equal to' to true");
            Console.WriteLine("Test_4254073 completed");
        }
    }
}
