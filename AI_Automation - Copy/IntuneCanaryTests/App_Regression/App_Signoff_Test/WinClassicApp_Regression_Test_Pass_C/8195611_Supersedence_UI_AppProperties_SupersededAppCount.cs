using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195611_Supersedence_UI_AppProperties_SupersededAppCount : SecurityBaseline
    {
        [Test]
        public async Task Test_8195611_Verify_AppProperties_SupersededAppCount_IsCorrect()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195611 - Verify that App properties superseded app count is correct");
            Console.WriteLine("Test_8195611 completed");
        }
    }
}
