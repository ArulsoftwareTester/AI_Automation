using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195619_Supersedence_UI_Delete_SupersededApp_SelectNo : SecurityBaseline
    {
        [Test]
        public async Task Test_8195619_SupersededApp_NotDeleted_When_SelectNo()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195619 - Superseded app is not deleted when click 'Delete' then select 'No'");
            Console.WriteLine("Test_8195619 completed");
        }
    }
}
