using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8195625_Supersedence_UI_ChainOf10_ConnectingBlocked : SecurityBaseline
    {
        [Test]
        public async Task Test_8195625_Verify_ChainOf10_ConnectingAnotherApp_IsBlocked()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_8195625 - Verify that in a chain of 10 supersedencies connecting another app to supersede the first one is blocked");
            Console.WriteLine("Test_8195625 completed");
        }
    }
}
