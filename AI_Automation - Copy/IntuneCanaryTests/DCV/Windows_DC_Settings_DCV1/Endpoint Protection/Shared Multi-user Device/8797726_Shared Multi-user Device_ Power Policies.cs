using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797726_Shared_Multi_user_Device_Power_Policies : SecurityBaseline
    {
        [Test]
        public async Task Test_8797726_Shared_Multi_user_Device_Power_Policies()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797726 completed");
        }
    }
}
