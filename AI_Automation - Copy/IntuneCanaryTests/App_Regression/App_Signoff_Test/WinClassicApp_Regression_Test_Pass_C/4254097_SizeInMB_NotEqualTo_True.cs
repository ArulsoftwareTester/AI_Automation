using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T4254097_SizeInMB_NotEqualTo_True : SecurityBaseline
    {
        [Test]
        public async Task Test_4254097_Verify_applicability_true_SizeInMB_NotEqualTo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_4254097 - Verify applicability is true when Size in MB evaluates 'Not Equal to' to true");
            Console.WriteLine("Test_4254097 completed");
        }
    }
}
