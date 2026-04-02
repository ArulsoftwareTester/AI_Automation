using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T10035354_DC_Enable_sorting_function_for_the_DC_profile_list
    {
        [Test]
        public async Task Test_10035354_DC_Enable_sorting_function_for_the_DC_profile_list()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_10035354 completed");
        }
    }
}
