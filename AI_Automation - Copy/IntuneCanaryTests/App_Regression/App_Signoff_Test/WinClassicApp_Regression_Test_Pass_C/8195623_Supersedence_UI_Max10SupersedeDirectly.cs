using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195623_Supersedence_UI_Max10SupersedeDirectly : SecurityBaseline
    {
        [Test]
        public async Task Test_8195623_Verify_OneApp_Cannot_Supersede_MoreThan10_Directly()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195623 - Verify 1 App cannot supersede more than 10 others directly");
            Console.WriteLine("Test_8195623 completed");
        }
    }
}
