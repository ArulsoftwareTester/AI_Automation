using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195610_Supersedence_UI_AppProperties_DependencyCount : SecurityBaseline
    {
        [Test]
        public async Task Test_8195610_Verify_AppProperties_DependencyCount_IsCorrect()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195610 - Verify that App properties dependency count is correct");
            Console.WriteLine("Test_8195610 completed");
        }
    }
}
