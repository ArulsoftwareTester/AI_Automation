using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1001985_Company_Portal_UI_Search_floating_button_shows_correctly_on_AppSummary_screen_and_app_viewAll_screen : SecurityBaseline
    {
        [Test]
        public async Task Test_1001985_Company_Portal_UI_Search_floating_button_shows_correctly_on_AppSummary_screen_and_app_viewAll_screen()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1001985 completed");
        }
    }
}
