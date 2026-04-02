using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797607_Shared_Multi_user_Device_Shared_PC_mode : SecurityBaseline
    {
        [Test]
        public async Task Test_8797607_Shared_Multi_user_Device_Shared_PC_mode()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797607 completed");
        }
    }
}
