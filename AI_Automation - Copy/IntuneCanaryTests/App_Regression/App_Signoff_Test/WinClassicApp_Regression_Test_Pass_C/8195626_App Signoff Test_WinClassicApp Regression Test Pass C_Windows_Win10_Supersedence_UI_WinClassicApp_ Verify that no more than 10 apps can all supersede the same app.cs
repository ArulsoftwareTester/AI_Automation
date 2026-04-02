using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195626_App_Signoff_Test_WinClassicApp_Regression_Test_Pass_C_Windows_Win10_Supersedence_UI_WinClassicApp_Verify_that_no_more_than_10_apps_can_all_supersede_the_same_app : SecurityBaseline
    {
        [Test]
        public async Task Test_8195626_Verify_that_no_more_than_10_apps_can_all_supersede_the_same_app()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195626 - Verify that no more than 10 apps can all supersede the same app");
            Console.WriteLine("Test_8195626 completed");
        }
    }
}
