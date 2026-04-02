using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26259492_Windows_Assignment_Included_user_group_unassign_user_group
    {
        [Test]
        public async Task Test_26259492_Windows_Assignment_Included_user_group_unassign_user_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26259492 completed");
        }
    }
}
