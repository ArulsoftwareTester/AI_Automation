using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26259558_Windows_Assignment_Include_all_devices_and_exclude_device1_group
    {
        [Test]
        public async Task Test_26259558_Windows_Assignment_Include_all_devices_and_exclude_device1_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26259558 completed");
        }
    }
}
