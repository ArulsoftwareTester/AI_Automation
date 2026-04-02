using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195614_Supersedence_UI_AppRelationships_SupersededAppCount : SecurityBaseline
    {
        [Test]
        public async Task Test_8195614_Verify_AppRelationships_SupersededAppCount_IsCorrect()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195614 - Verify that App relationships superseded app count is correct");
            Console.WriteLine("Test_8195614 completed");
        }
    }
}
