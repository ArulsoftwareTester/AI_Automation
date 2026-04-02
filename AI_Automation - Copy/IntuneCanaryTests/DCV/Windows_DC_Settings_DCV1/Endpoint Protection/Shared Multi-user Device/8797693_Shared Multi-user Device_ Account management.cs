using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797693_Shared_Multi_user_Device_Account_management : SecurityBaseline
    {
        [Test]
        public async Task Test_8797693_Shared_Multi_user_Device_Account_management()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797693 completed");
        }
    }
}
