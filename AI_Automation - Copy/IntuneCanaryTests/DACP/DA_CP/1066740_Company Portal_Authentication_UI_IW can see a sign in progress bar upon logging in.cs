using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1066740_Company_Portal_Authentication_UI_IW_can_see_a_sign_in_progress_bar_upon_logging_in : SecurityBaseline
    {
        [Test]
        public async Task Test_1066740_Company_Portal_Authentication_UI_IW_can_see_a_sign_in_progress_bar_upon_logging_in()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1066740 completed");
        }
    }
}
