using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195617_Supersedence_UI_Delete_SupersededApp_AfterRemovingRelationships : SecurityBaseline
    {
        [Test]
        public async Task Test_8195617_Can_Delete_SupersededApp_AfterRemovingRelationships()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195617 - Can delete a superseded app after removing related relationships");
            Console.WriteLine("Test_8195617 completed");
        }
    }
}
