using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8959794_Device_Restrictions_Personalization_Desktop_background_picture_URL_Desktop_only : SecurityBaseline
    {
        [Test]
        public async Task Test_8959794_Device_Restrictions_Personalization_Desktop_background_picture_URL_Desktop_only()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8959794 completed");
        }
    }
}
