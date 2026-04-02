using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8906680_Endpoint_Protection_Localdevicesecurityoptions_Accounts_Add_new_Microsoft_accounts : SecurityBaseline
    {
        [Test]
        public async Task Test_8906680_Endpoint_Protection_Localdevicesecurityoptions_Accounts_Add_new_Microsoft_accounts()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8906680 completed");
        }
    }
}
