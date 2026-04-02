using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4180346_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4180346";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_DependentApp_4180346";
        protected override string TestDescription => @"[IntuneSA]:[Windows][Win10]:[DependentApp][Dependencies][E2E][AutoInstall]:[WinClassicApp]:[Required][UserGroup]: A -> B (AutoInst no). If AppB is not present, App A's installation should fail.";

        [Test]
        public async Task TestMethod_4180346_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

