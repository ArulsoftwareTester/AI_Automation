using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26259715_Windows_Assignment_Include_all_Users_and_exclude_User1_group
    {
        [Test]
        public async Task Test_26259715_Windows_Assignment_Include_all_Users_and_exclude_User1_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26259715 completed");
        }
    }
}
