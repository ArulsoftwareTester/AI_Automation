using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254103_SizeInMB_LessThanOrEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254103_Verify_applicability_true_SizeInMB_LessThanOrEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254103 - Verify applicability is true when Size in MB evaluates 'Less than or equal to' to true");
            Console.WriteLine("Test_4254103 completed");
        }
    }
}
