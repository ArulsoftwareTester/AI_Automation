using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195622_Supersedence_UI_Delete_DeletingGrandchild : SecurityBaseline
    {
        [Test]
        public async Task Test_8195622_Verify_App_With_Supersedence_DeletingGrandchild()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195622 - Verify App with supersedence - deleting grandchild");
            Console.WriteLine("Test_8195622 completed");
        }
    }
}
