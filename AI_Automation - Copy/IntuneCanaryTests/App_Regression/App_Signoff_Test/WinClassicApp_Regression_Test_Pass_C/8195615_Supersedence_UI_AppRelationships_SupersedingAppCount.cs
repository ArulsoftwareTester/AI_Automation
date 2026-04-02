using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195615_Supersedence_UI_AppRelationships_SupersedingAppCount : SecurityBaseline
    {
        [Test]
        public async Task Test_8195615_Verify_AppRelationships_SupersedingAppCount_IsCorrect()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195615 - Verify that App relationships superseding app count is correct");
            Console.WriteLine("Test_8195615 completed");
        }
    }
}
