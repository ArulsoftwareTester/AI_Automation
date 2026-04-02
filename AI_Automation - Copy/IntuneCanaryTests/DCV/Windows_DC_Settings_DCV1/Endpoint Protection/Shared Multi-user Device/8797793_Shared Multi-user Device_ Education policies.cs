using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797793_Shared_Multi_user_Device_Education_policies : SecurityBaseline
    {
        [Test]
        public async Task Test_8797793_Shared_Multi_user_Device_Education_policies()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797793 completed");
        }
    }
}
