using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797708_Shared_Multi_user_Device_Local_Storage : SecurityBaseline
    {
        [Test]
        public async Task Test_8797708_Shared_Multi_user_Device_Local_Storage()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797708 completed");
        }
    }
}
