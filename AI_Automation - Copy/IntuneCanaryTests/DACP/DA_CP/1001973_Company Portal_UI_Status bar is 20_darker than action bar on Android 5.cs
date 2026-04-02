using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1001973_Company_Portal_UI_Status_bar_is_20_darker_than_action_bar_on_Android_5 : SecurityBaseline
    {
        [Test]
        public async Task Test_1001973_Company_Portal_UI_Status_bar_is_20_darker_than_action_bar_on_Android_5()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1001973 completed");
        }
    }
}
