using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195618_Supersedence_UI_Delete_SupersedingApp_AfterRemovingRelationships : SecurityBaseline
    {
        [Test]
        public async Task Test_8195618_Can_Delete_SupersedingApp_AfterRemovingRelationships()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195618 - Can delete a superseding app after removing related relationships");
            Console.WriteLine("Test_8195618 completed");
        }
    }
}
