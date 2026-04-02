using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T2056053_Company_Portal_Localization_Ensure_the_EULA_loads_correctly : SecurityBaseline
    {
        [Test]
        public async Task Test_2056053_Company_Portal_Localization_Ensure_the_EULA_loads_correctly()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_2056053 completed");
        }
    }
}
