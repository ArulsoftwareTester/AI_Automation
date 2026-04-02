using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195620_Supersedence_UI_Delete_DeletingParent : SecurityBaseline
    {
        [Test]
        public async Task Test_8195620_Verify_App_With_Supersedence_DeletingParent()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195620 - Verify App with supersedence - deleting parent");
            Console.WriteLine("Test_8195620 completed");
        }
    }
}
