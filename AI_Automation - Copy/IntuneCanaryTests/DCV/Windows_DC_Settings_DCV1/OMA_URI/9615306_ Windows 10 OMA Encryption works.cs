using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T9615306_Windows_10_OMA_Encryption_works : SecurityBaseline
    {
        [Test]
        public async Task Test_9615306_Windows_10_OMA_Encryption_works()
        {
            await IPLogin(Page);
            Console.WriteLine("Test_9615306 completed");
        }
    }
}
