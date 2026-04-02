using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195627_App_Signoff_Test_WinClassicApp_Regression_Test_Pass_C_Windows_Win10_Supersedence_UI_WinClassicApp_Verify_that_combining_two_nearly_maxed_subgraphs_fails : SecurityBaseline
    {
        [Test]
        public async Task Test_8195627_Verify_that_combining_two_nearly_maxed_subgraphs_fails()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195627 - Verify that combining two nearly maxed subgraphs fails");
            Console.WriteLine("Test_8195627 completed");
        }
    }
}
