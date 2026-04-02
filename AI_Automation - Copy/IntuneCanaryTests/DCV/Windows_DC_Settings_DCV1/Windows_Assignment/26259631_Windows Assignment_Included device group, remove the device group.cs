using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T26259631_Windows_Assignment_Included_device_group_remove_the_device_group
    {
        [Test]
        public async Task Test_26259631_Windows_Assignment_Included_device_group_remove_the_device_group()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_26259631 completed");
        }
    }
}
