using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26268330_Windows_Assignment_Assign_all_users_all_devices_with_filter
    {
        [Test]
        public async Task Test_26268330_Windows_Assignment_Assign_all_users_all_devices_with_filter()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26268330 completed");
        }
    }
}
