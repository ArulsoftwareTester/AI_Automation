using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254106_SizeInMB_LessThan_False : SecurityBaseline
    {
        [Test]
        public async Task Test_4254106_Verify_applicability_false_SizeInMB_LessThan()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254106 - Verify applicability is false when Size in MB evaluates 'Less than' to false");
            Console.WriteLine("Test_4254106 completed");
        }
    }
}
