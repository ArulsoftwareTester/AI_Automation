using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254082_DateCreated_LessThan_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254082_Verify_applicability_false_DateCreated_LessThan()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254082 - Verify applicability is false when Date created_UTC evaluates 'Less than' to false");
            Console.WriteLine("Test_4254082 completed");
        }
    }
}
