using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8797771_Shared_Multi_user_Device_Signin_when_PC_wakes : SecurityBaseline
    {
        [Test]
        public async Task Test_8797771_Shared_Multi_user_Device_Signin_when_PC_wakes()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8797771 completed");
        }
    }
}
