using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797728_Shared_Multi_user_Device_Sleep_time_out_in_seconds : SecurityBaseline
    {
        [Test]
        public async Task Test_8797728_Shared_Multi_user_Device_Sleep_time_out_in_seconds()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797728 completed");
        }
    }
}
