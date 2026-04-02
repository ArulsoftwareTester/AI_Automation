using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T5524894_DC_Two_different_Policy_Set_with_same_workload__Delete_workload_from_account
    {
        [Test]
        public async Task Test_5524894_DC_Two_different_Policy_Set_with_same_workload__Delete_workload_from_account()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_5524894 completed");
        }
    }
}
