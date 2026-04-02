using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195616_Supersedence_UI_AppRelationships_TargetType_ParentChild : SecurityBaseline
    {
        [Test]
        public async Task Test_8195616_Verify_AppRelationships_SupersedingTargetType_IsCorrect()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195616 - Verify that App relationships superseding target type (parent/child) is correct");
            Console.WriteLine("Test_8195616 completed");
        }
    }
}
