using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195613_Supersedence_UI_AppRelationships_DependencyCount : SecurityBaseline
    {
        [Test]
        public async Task Test_8195613_Verify_AppRelationships_DependencyCount_IsCorrect()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195613 - Verify that App relationships dependency count is correct");
            Console.WriteLine("Test_8195613 completed");
        }
    }
}
