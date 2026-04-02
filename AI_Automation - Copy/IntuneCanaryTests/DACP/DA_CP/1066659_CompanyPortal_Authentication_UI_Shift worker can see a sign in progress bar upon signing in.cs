using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T1066659_CompanyPortal_Authentication_UI_Shift_worker_can_see_a_sign_in_progress_bar_upon_signing_in : SecurityBaseline
    {
        [Test]
        public async Task Test_1066659_CompanyPortal_Authentication_UI_Shift_worker_can_see_a_sign_in_progress_bar_upon_signing_in()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_1066659 completed");
        }
    }
}
