using NUnit.Framework;
using System.Threading.Tasks;

namespace IntuneCanaryTests
{
    [TestFixture]
    public class Test_4179741_Non_Enus_WinClassic_Sanity : NonEnUsWinClassicSanityTestBase
    {
        protected override string TestId => @"4179741";
        protected override string TestTitle => @"Test set A - Non-Enus WinClassic Sanity - Win32App_Create_4179741";
        protected override string TestDescription => @"[IntuneSA]:[Win10]:[E2E][Create]:[WinClassicApp]:[Available]:[DeviceGroup]:[IntuneWin]:Verify App does not show in IWPortal/CP for all Windows devices in group";

        [Test]
        public async Task TestMethod_4179741_Non_Enus_WinClassic_Sanity()
        {
            await RunTestAsync();
        }
    }
}

