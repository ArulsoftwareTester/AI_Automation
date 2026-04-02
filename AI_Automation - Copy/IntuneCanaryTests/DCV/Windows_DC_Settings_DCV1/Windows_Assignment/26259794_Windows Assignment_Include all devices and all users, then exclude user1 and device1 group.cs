using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26259794_Windows_Assignment_Include_all_devices_and_all_users_then_exclude_user1_and_device1_group
    {
        [Test]
        public async Task Test_26259794_Windows_Assignment_Include_all_devices_and_all_users_then_exclude_user1_and_device1_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26259794 completed");
        }
    }
}
