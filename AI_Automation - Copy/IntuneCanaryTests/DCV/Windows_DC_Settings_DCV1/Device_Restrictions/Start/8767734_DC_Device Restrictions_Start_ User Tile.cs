using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class T8767734_DC_Device_Restrictions_Start_User_Tile
    {
        [Test]
        public async Task Test_8767734_DC_Device_Restrictions_Start_User_Tile()
        {
            var securityBaseline = new SecurityBaseline();
            await securityBaseline.IPLogin(null!);
            Console.WriteLine("Test_8767734 completed");
        }
    }
}
