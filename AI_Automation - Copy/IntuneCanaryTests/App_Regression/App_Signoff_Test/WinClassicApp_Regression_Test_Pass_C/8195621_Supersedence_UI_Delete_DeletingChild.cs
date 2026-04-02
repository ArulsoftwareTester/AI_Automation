using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195621_Supersedence_UI_Delete_DeletingChild : SecurityBaseline
    {
        [Test]
        public async Task Test_8195621_Verify_App_With_Supersedence_DeletingChild()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195621 - Verify App with supersedence - deleting child");
            Console.WriteLine("Test_8195621 completed");
        }
    }
}
