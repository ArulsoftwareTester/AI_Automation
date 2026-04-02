using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797672_Shared_Multi_user_Device_Guest_account : SecurityBaseline
    {
        [Test]
        public async Task Test_8797672_Shared_Multi_user_Device_Guest_account()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797672 completed");
        }
    }
}
