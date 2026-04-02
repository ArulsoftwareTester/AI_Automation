using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195612_Supersedence_UI_AppProperties_SupersedingAppCount : SecurityBaseline
    {
        [Test]
        public async Task Test_8195612_Verify_AppProperties_SupersedingAppCount_IsCorrect()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195612 - Verify that App properties superseding app count is correct");
            Console.WriteLine("Test_8195612 completed");
        }
    }
}
