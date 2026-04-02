using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26263408_Windows_Assignment_Assign_group_with_include_exclude_filter
    {
        [Test]
        public async Task Test_26263408_Windows_Assignment_Assign_group_with_include_exclude_filter()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26263408 completed");
        }
    }
}
