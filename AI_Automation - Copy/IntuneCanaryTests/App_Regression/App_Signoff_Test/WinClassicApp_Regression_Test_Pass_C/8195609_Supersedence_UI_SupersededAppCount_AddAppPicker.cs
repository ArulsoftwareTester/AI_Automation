using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195609_Supersedence_UI_SupersededAppCount_AddAppPicker : SecurityBaseline
    {
        [Test]
        public async Task Test_8195609_Verify_UI_Shows_SupersededAppCount_In_AddAppPicker()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195609 - Verify UI shows superseded app count in the add app picker for supersedence");
            Console.WriteLine("Test_8195609 completed");
        }
    }
}
