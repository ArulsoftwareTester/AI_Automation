using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4179740_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4179740";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Create_4179740";
        protected override string TestDescription => @"[IntuneSA]:[Win10]:[E2E][Create]:[WinClassicApp]:[Available]:[AllDeviceGroup]:[IntuneWin]:Verify All Devices is a selection in the UI in 2211+";

        [Test]
        public async Task TestMethod_4179740_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

